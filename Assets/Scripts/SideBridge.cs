using DG.Tweening;
using UnityEngine;

public class SideBridge : MonoBehaviour
{
    [Header("Moving settings")]
    [SerializeField] private Vector3 endPosition;

    [Header("Timing settings")]
    [SerializeField] private float toEndPositionMoveTime = 2f;

    [Header("Ease settings")]
    [SerializeField] private Ease toEndPointEase = Ease.Linear;

    private void OnDestroy()
    {
        DOTween.KillAll();
    }

    private void OnEnable()
    {
        ActivationButton.OnActivated += HandleActivation;
    }

    private void OnDisable()
    {
        ActivationButton.OnActivated -= HandleActivation;
    }

    private void HandleActivation(ActivationButton button, GameObject gObj)
    {
        if (gObj != gameObject)
            return;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMove(endPosition, toEndPositionMoveTime).SetEase(toEndPointEase));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        var startPos = transform.localPosition;

        if (transform.parent != null)
        {
            startPos = transform.parent.TransformPoint(startPos);
        }

        Gizmos.DrawSphere(startPos, 0.2f);

        var endPos = endPosition;

        if (transform.parent != null)
        {
            endPos = transform.parent.TransformPoint(endPosition);
        }

        Gizmos.DrawSphere(endPos, 0.2f);
        Gizmos.DrawLine(startPos, endPos);
    }

}
