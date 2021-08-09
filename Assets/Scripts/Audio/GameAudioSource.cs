using System;
using UnityEngine;

public class GameAudioSource : MonoBehaviour
{
    private AudioSource audioSource;
    private SFXInfo sfxInfo;

    private Action<GameAudioSource> onKill;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        if(!audioSource.isPlaying)
        {
            KillSelf();
        }
    }

    private void KillSelf()
    {
        onKill?.Invoke(this);

        Destroy(audioSource);
        Destroy(this);
    }

    public void Setup(SFXInfo sfxInfo, float globalSFXVolume)
    {
        this.sfxInfo = sfxInfo;
        audioSource.clip = sfxInfo.clip;
        audioSource.volume = sfxInfo.volume * globalSFXVolume;
        audioSource.pitch = sfxInfo.pitch;
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void SetVolume(float globalSFXVolume)
    {
        audioSource.volume = sfxInfo.volume * globalSFXVolume;
    }

    public GameAudioSource OnKill(Action<GameAudioSource> onKill)
    {
        this.onKill = onKill;

        return this;
    }
}
