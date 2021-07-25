using DG.Tweening;
using UnityEngine;

public class Colon : MonoBehaviour
{
    [SerializeField] private Vector3 startScale = Vector3.one;
    [SerializeField] private Vector3 endScale = Vector3.one;
    [SerializeField] private float toStartDuration = 1;
    [SerializeField] private float toEndDuration = 1;
    [SerializeField] private float startDelay;
    [SerializeField] private float endDelay;
    [SerializeField] private Ease startEase = Ease.Linear;
    [SerializeField] private Ease endEase = Ease.Linear;


    private void OnDestroy()
    {
        DOTween.KillAll();
    }

    private void Start()
    {
        StartAnimation();
    }

    private void StartAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(startDelay);
        sequence.Append(transform.DOScale(endScale, toEndDuration).SetEase(endEase));
        sequence.AppendInterval(endDelay);
        sequence.Append(transform.DOScale(startScale, toStartDuration).SetEase(startEase));
        sequence.SetLoops(-1);
    }
}
