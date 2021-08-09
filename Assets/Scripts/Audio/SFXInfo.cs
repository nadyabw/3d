using System;
using UnityEngine;

[Serializable]
public class SFXInfo
{
    [HideInInspector]
    [SerializeField] private string name;
    public AudioClip clip;
    public SFXType type;
    public float volume = 1f;
    public float pitch = 1f;

    public void UpdateName()
    {
        name = type.ToString();
    }
}
