using System;
using DG.Tweening;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;

    [Header("Timing")]
    [SerializeField] private float startPositionDelay;
    [SerializeField] private float endPositionDelay;
    [SerializeField] private float toEndPositionMoveTime;
    [SerializeField] private float toStartPositionMoveTime;
    
    [Header("Ease")]
    [SerializeField] private Ease toEndPointEase = Ease.Linear;
    [SerializeField] private Ease toStartPointEase = Ease.Linear;

    private void OnDestroy()
    {
        DOTween.KillAll();
    }

    private void Start()
    {
        StartAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Player.Instance.IsSameObject(other.gameObject))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Player.Instance.IsSameObject(other.gameObject))
        {
            other.transform.SetParent(null);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        var startPos = startPosition;

        if (transform.parent != null)
        {
            startPos = transform.parent.TransformPoint(startPosition);
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

    private void StartAnimation()
    {
        Sequence sequence = DOTween.Sequence().SetUpdate(UpdateType.Fixed);
        sequence.AppendInterval(startPositionDelay);
        sequence.Append(transform.DOLocalMove(endPosition, toEndPositionMoveTime).SetEase(toEndPointEase));
        sequence.AppendInterval(endPositionDelay);
        sequence.Append(transform.DOLocalMove(startPosition, toStartPositionMoveTime).SetEase(toStartPointEase));
        sequence.SetLoops(-1);
    }
}
