using DG.Tweening;
using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Header("Moving settings")]
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;

    [Header("Timing settings")]
    [SerializeField] private float startPositionDelay = 0.5f;
    [SerializeField] private float toEndPositionMoveTime = 1f;
    [SerializeField] private float toStartPositionMoveTime = 2f;
    [SerializeField] private float movingUpInterval = 2f;

    [Header("Ease settings")]
    [SerializeField] private Ease toEndPointEase = Ease.Linear;
    [SerializeField] private Ease toStartPointEase = Ease.Linear;

    [Header("Shake settings")]
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeStrength;
    [SerializeField] private int shakeVibro;
    [SerializeField] private float shakeRandom;


    private bool isFalling = false;
    private bool isPlayerStaying = false;

    private void OnDestroy()
    {
        DOTween.KillAll();
    }

    private void StartFalling()
    {
        isFalling = true;

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(startPositionDelay);
        sequence.Append(transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibro, shakeRandom));
        sequence.AppendCallback(HandleFallingBegin);
        sequence.Append(transform.DOLocalMove(endPosition, toEndPositionMoveTime).SetEase(toEndPointEase));
        sequence.AppendCallback(HandleFallingComplete);
    }

    private void StartMovingUp()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMove(startPosition, toStartPositionMoveTime).SetEase(toStartPointEase));
        sequence.AppendCallback(HandleMovingUpComplete);
    }

    private void HandleFallingBegin()
    {
        if(isPlayerStaying)
        {
            Player player = Player.Instance;
            player.DisableMovement();
            player.transform.SetParent(transform);
        }
    }

    private void HandleFallingComplete()
    {
        Player player = Player.Instance;
        if (player.transform.parent == transform)
        {
            Player.Instance.HandleDeath();
        }

        StartCoroutine(UpdateMovingUp());
    }

    private void HandleMovingUpComplete()
    {
        isFalling = false;
    }

    private IEnumerator UpdateMovingUp()
    {
        yield return new WaitForSeconds(movingUpInterval);

        StartMovingUp();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Player.Instance.IsSameObject(other.gameObject))
        {
            isPlayerStaying = true;

            if(!isFalling)
            {
                StartFalling();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Player.Instance.IsSameObject(other.gameObject))
        {
            isPlayerStaying = false;
        }
    }
}
