using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AudioSources
{
    public AudioClip musicClip;
    public AudioClip sfxClip;
}


public class AudioManager : SingletonPattern<AudioManager>
{
    public AudioSource music;
    public AudioSource SFX;

    protected override void Awake()
    {
        base.Awake();
    }

    public void ChangeTracks(AudioSources sources)
    {
        music.clip = sources.musicClip;
        SFX.clip = sources.sfxClip;
    }
}
