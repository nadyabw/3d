using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private float animDuration = 0.5f;

    [Header("Ease")]
    [SerializeField] private Ease animEase = Ease.Linear;

    private Sequence animationSequence;

    private void Start()
    {
        LevelManager.Instance.HandleCoinCreated();

        StartAnimation();
    }

    private void StartAnimation()
    {
        animationSequence = DOTween.Sequence();
        animationSequence.Append(transform.DOLocalRotate(new Vector3(90f, 90f, 0), animDuration).SetEase(animEase));
        animationSequence.Append(transform.DOLocalRotate(new Vector3(90f, 180f, 0), animDuration).SetEase(animEase));
        animationSequence.Append(transform.DOLocalRotate(new Vector3(90f, 270f, 0), animDuration).SetEase(animEase));
        animationSequence.Append(transform.DOLocalRotate(new Vector3(90f, 0, 0), animDuration).SetEase(animEase));
        animationSequence.SetLoops(-1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Player.Instance.IsSameObject(other.gameObject))
        {
            LevelManager.Instance.HandleCoinCollected();

            animationSequence.Kill();
            Destroy(gameObject);
        }
    }
}
