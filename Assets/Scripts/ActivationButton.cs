using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class ActivationButton : MonoBehaviour
{
    [Header("Base settings")]
    [SerializeField] private GameObject targetObject;
    [SerializeField] private float deactivationInterval = 5f;
    [SerializeField] private Vector3 normalScale = Vector3.one;
    [SerializeField] private Vector3 pressedScale = Vector3.one;
    [SerializeField] private float activationDuration = 1f;
    [SerializeField] private float deactivationDuration = 1f;

    [Header("Ease settings")]
    [SerializeField] private Ease activationEase = Ease.Linear;
    [SerializeField] private Ease deactivationEase = Ease.Linear;


    private bool isActivated = false;

    public static event Action<ActivationButton, GameObject> OnActivated;
    public static event Action<ActivationButton, GameObject> OnDeactivated;

    private void StartActivation()
    {
        isActivated = true;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(pressedScale, activationDuration).SetEase(activationEase));
        sequence.AppendCallback(HandleActivationComplete);
    }

    private void StartDeactivation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(normalScale, deactivationDuration).SetEase(deactivationEase));
        sequence.AppendCallback(HandleDeactivationComplete);
    }

    private void HandleActivationComplete()
    {
        OnActivated?.Invoke(this, targetObject);
        StartCoroutine(UpdateDeactivation());
    }

    private IEnumerator UpdateDeactivation()
    {
        yield return new WaitForSeconds(deactivationInterval);

        StartDeactivation();
    }

    private void HandleDeactivationComplete()
    {
        OnDeactivated?.Invoke(this, targetObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Player.Instance.IsSameObject(other.gameObject) && !isActivated)
        {
            StartActivation();
        }
    }

    public void Enable()
    {
        isActivated = false;
    }
}
