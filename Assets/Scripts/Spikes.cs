using DG.Tweening;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition = Vector3.one;
    [SerializeField] private Vector3 endPosition = Vector3.one;
    [SerializeField] private float toStartDuration = 1;
    [SerializeField] private float toEndDuration = 1;
    [SerializeField] private float startDelay;
    [SerializeField] private float endDelay;
    [SerializeField] private Ease startEase = Ease.Linear;
    [SerializeField] private Ease endEase = Ease.Linear;

    [Header("Shake")]
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeStrength;
    [SerializeField] private int shakeVibro;
    [SerializeField] private float shakeRandom;
    [SerializeField] private Vector3 shakeRight;
    [SerializeField] private Vector3 shakeLeft;
    [SerializeField] private int shakeLoops;


    private void OnDestroy()
    {
        DOTween.KillAll();
    }

    private void Awake()
    {
        transform.localPosition = startPosition;
    }

    private void Start()
    {
        StartAnimation();
    }

    private void StartAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(startDelay);

        // sequence.Append(transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibro, shakeRandom));
        sequence.Append(CreateShake());
        sequence.Append(transform.DOLocalMove(endPosition, toEndDuration).SetEase(endEase));
        sequence.AppendInterval(endDelay);
        sequence.Append(transform.DOLocalMove(startPosition, toStartDuration).SetEase(startEase));
        sequence.SetLoops(-1);
    }

    private Sequence CreateShake()
    {
        Sequence shakeS = DOTween.Sequence();
        shakeS.Append(transform.DOLocalMove(shakeRight, shakeDuration));
        shakeS.Append(transform.DOLocalMove(startPosition, shakeDuration));
        shakeS.Append(transform.DOLocalMove(shakeLeft, shakeDuration));
        shakeS.Append(transform.DOLocalMove(startPosition, shakeDuration));
        shakeS.SetLoops(shakeLoops);

        return shakeS;
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
