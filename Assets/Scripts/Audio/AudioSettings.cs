using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[ CreateAssetMenu (fileName = nameof(AudioSettings), menuName = "Audio/Audio settings")]
public class AudioSettings : ScriptableObject
{
    [SerializeField] private SFXInfo[] sfx;
    [SerializeField] private AudioClip[] bgmTracks;

    private int currentMusicTrackNumber;

    private Dictionary<SFXType, SFXInfo> sfxMap = new Dictionary<SFXType, SFXInfo>();

    private void OnEnable()
    {
        FillMap();
    }

    private void OnValidate()
    {
        if (sfx == null)
            return;

        foreach (var sfxInfo in sfx)
        {
            sfxInfo.UpdateName();
        }
    }

    private void FillMap()
    {
        sfxMap.Clear();

        if (sfx == null)
            return;

        foreach (var sfxInfo in sfx)
        {
            var type = sfxInfo.type;
            if(!sfxMap.ContainsKey(type))
            {
                sfxMap.Add(type, sfxInfo);
            }
        }

    }

    public AudioClip GetAudioClip(SFXType type)
    {
        if(sfxMap.ContainsKey(type))
        {
            return sfxMap[type].clip;
        }
        else
        {
            return null;
        }
    }

    public SFXInfo GetSFXInfo(SFXType type)
    {
        if (sfxMap.ContainsKey(type))
        {
            return sfxMap[type];
        }
        else
        {
            return null;
        }
    }

    public AudioClip GetRandomMusicTrack()
    {
        if ((bgmTracks == null) || (bgmTracks.Length == 0)) 
            return null;

        currentMusicTrackNumber = Random.Range(0, bgmTracks.Length);
        return bgmTracks[currentMusicTrackNumber];
    }

    public AudioClip GetNextMusicTrack()
    {
        if ((bgmTracks == null) || (bgmTracks.Length == 0))
            return null;

        currentMusicTrackNumber++;
        if (currentMusicTrackNumber >= bgmTracks.Length)
            currentMusicTrackNumber = 0;

        return bgmTracks[currentMusicTrackNumber];
    }
}
