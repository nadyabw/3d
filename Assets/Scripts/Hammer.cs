using DG.Tweening;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField] private float phaseTime = 0.75f;
    [SerializeField] private float startAngle = 110f;
    [SerializeField] private float endAngle = 250f;
    [SerializeField] private float endPhaseDelay = 0.5f;
    [SerializeField] private Ease animEase = Ease.Linear;

    private Sequence animationSequence;

    private void OnDestroy()
    {
        DOTween.Kill(animationSequence);
    }

    private void Start()
    {
        StartAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Player.Instance.IsSameObject(other.gameObject))
        {
            Player.Instance.HandleDeath();
        }
    }

    private void StartAnimation()
    {
        animationSequence = DOTween.Sequence();
        animationSequence.Append(transform.DOLocalRotate(new Vector3(0f, 0f, endAngle), phaseTime).SetEase(animEase));
        animationSequence.AppendInterval(endPhaseDelay);
        animationSequence.Append(transform.DOLocalRotate(new Vector3(0f, 0f, startAngle), phaseTime).SetEase(animEase));
        animationSequence.AppendInterval(endPhaseDelay);
        animationSequence.SetLoops(-1);
    }
}
