using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseView : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Button continueButton;
    [SerializeField] private Vector2 normalPosition;
    [SerializeField] private Vector2 hiddenPosition;
    [SerializeField] private float showDuration = 1f;
    [SerializeField] private float hideDuration = 1f;
    [SerializeField] private Ease showEase = Ease.OutBounce;
    [SerializeField] private Ease hideEase = Ease.Linear;

    private Sequence showSequence;
    private Sequence hideSequence;

    public static event Action OnClose;

    void Start()
    {
        HidePanelImmediately();

        musicSlider.value = AudioManager.Instance.MusicVolume;
        sfxSlider.value = AudioManager.Instance.SFXVolume;

        continueButton.onClick.AddListener(ContinueClickHandler);
        musicSlider.onValueChanged.AddListener(MusicVolumeChangeHandler);
        sfxSlider.onValueChanged.AddListener(SFXVolumeChangeHandler);
    }

    private void ContinueClickHandler()
    {
        OnClose?.Invoke();
    }

    private void MusicVolumeChangeHandler(float value)
    {
        AudioManager.Instance.MusicVolume = value;
    }

    private void SFXVolumeChangeHandler(float value)
    {
        AudioManager.Instance.SFXVolume = value;
    }

    private void EnableElements()
    {
        continueButton.enabled = true;
        sfxSlider.enabled = true;
        musicSlider.enabled = true;
    }

    private void DisableElements()
    {
        continueButton.enabled = false;
        sfxSlider.enabled = false;
        musicSlider.enabled = false;
    }

    public void ShowPanel()
    {
        hideSequence?.Kill();

        showSequence = DOTween.Sequence().SetUpdate(true);
        showSequence.Append(transform.DOLocalMove(normalPosition, showDuration).SetEase(showEase));
        showSequence.AppendCallback(EnableElements);
    }

    public void HidePanel()
    {
        showSequence?.Kill();

        hideSequence = DOTween.Sequence().SetUpdate(true);
        hideSequence.Append(transform.DOLocalMove(hiddenPosition, hideDuration).SetEase(hideEase));
        hideSequence.AppendCallback(DisableElements);
    }

    public void HidePanelImmediately()
    {
        transform.localPosition = hiddenPosition;
        DisableElements();
    }
}
