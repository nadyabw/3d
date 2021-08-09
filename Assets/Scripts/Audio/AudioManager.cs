using System.Collections.Generic;
using UnityEngine;

public enum SFXType {PlayerDeath, Jump, Door, Teleport, Shoot, EnemyDeath, EnemyHit}

public class AudioManager : MonoBehaviour
{
    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSettings settings;

    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    private static AudioManager instance;

    private List<GameAudioSource> sfxSources = new List<GameAudioSource>();

    public static AudioManager Instance { get => instance;}
    public float MusicVolume { get => musicVolume; set{ musicVolume = value; bgmSource.volume = musicVolume; } }
    public float SFXVolume
    {
        get => sfxVolume; 
        set
        {
            sfxVolume = value; 
            foreach (var aSource in sfxSources)
            {
                aSource.SetVolume(sfxVolume);
            }
        } 
    }

    private void Awake()
    {
        ReadSoundParams();

        instance = this;

        bgmSource.volume = musicVolume;
        bgmSource.loop = false;

        PlayRandomTrack();
    }

    private void OnDestroy()
    {
        SaveSoundParams();
    }

    private void Update()
    {
        if (bgmSource.isPlaying)
            return;

        PlayNextTrack();
    }

    public void PlaySFX(SFXType sfxType, Transform transform = null)
    {
        if ((transform != null) && (!transform.gameObject.activeSelf))
        {
            Debug.LogError("Sfx transform is null or not active");
            return;
        }

        var audioSrc = CreateAudioSource(transform);
        SetupAudioSource(audioSrc, settings.GetSFXInfo(sfxType));
    }

    private GameAudioSource CreateAudioSource(Transform transform)
    {
        var transformForAudioSrc = (transform == null) ? this.transform : transform;
        var audioSrc = transformForAudioSrc.gameObject.AddComponent<GameAudioSource>();
        audioSrc.OnKill(AudioSourceKilled);
        sfxSources.Add(audioSrc);

        return audioSrc;
    }

    private void AudioSourceKilled(GameAudioSource aSource)
    {
        sfxSources.Remove(aSource);
    }

    private void SetupAudioSource(GameAudioSource audioSrc, SFXInfo sfxInfo)
    {
        audioSrc.Setup(sfxInfo, sfxVolume);
        audioSrc.Play();
    }

    private AudioClip GetAudioClip(SFXType type)
    {
        return settings.GetAudioClip(type);
    }

    private void PlayRandomTrack()
    {
        bgmSource.clip = settings.GetRandomMusicTrack();
        bgmSource.Play();
    }

    private void PlayNextTrack()
    {
        bgmSource.clip = settings.GetNextMusicTrack();
        bgmSource.Play();
    }

    private void ReadSoundParams()
    {
        if (PlayerPrefs.HasKey(MusicVolumeKey))
            musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey);
        if (PlayerPrefs.HasKey(SFXVolumeKey))
            sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey);
    }

    private void SaveSoundParams()
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, musicVolume);
        PlayerPrefs.SetFloat(SFXVolumeKey, sfxVolume);
    }
}
