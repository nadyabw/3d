using DG.Tweening;
using UnityEngine;

public class ObstaclePress : MonoBehaviour
{
    [Header("Moving settings")]
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;

    [Header("Timing settings")]
    [SerializeField] private float toEndPositionMoveTime = 2f;
    [SerializeField] private float toStartPositionMoveTime = 2f;

    [Header("Ease settings")]
    [SerializeField] private Ease toEndPointEase = Ease.Linear;
    [SerializeField] private Ease toStartPointEase = Ease.Linear;

    [Header("Shake settings")]
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeStrength;
    [SerializeField] private int shakeVibro;
    [SerializeField] private float shakeRandom;

    private ActivationButton relatedButton;

    private void OnDestroy()
    {
        DOTween.KillAll();
    }

    private void OnEnable()
    {
        ActivationButton.OnActivated += HandleActivation;
        ActivationButton.OnDeactivated += HandleDeactivation;
    }

    private void OnDisable()
    {
        ActivationButton.OnActivated -= HandleActivation;
        ActivationButton.OnDeactivated -= HandleDeactivation;
    }

    private void HandleActivation(ActivationButton button, GameObject gObj)
    {
        if (gObj != gameObject)
            return;

        if(relatedButton == null)
            relatedButton = button;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMove(endPosition, toEndPositionMoveTime).SetEase(toEndPointEase));
    }

    private void HandleDeactivation(ActivationButton button, GameObject gObj)
    {
        if (gObj != gameObject)
            return;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibro, shakeRandom));
        sequence.Append(transform.DOLocalMove(startPosition, toStartPositionMoveTime).SetEase(toStartPointEase));
        sequence.AppendCallback(EnableRelatedButton);
    }

    private void EnableRelatedButton()
    {
        relatedButton.Enable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Player.Instance.IsSameObject(other.gameObject))
        {
            var player = other.GetComponent<Player>();
            player.HandleDeath();
        }
    }
}
