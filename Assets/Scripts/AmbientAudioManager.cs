using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudioManager : SingletonPattern<AmbientAudioManager>
{
    public AudioSource music;

    protected override void Awake()
    {
        base.Awake();
    }

    public void ChangeMusic(AudioClip clip)
    {
        music.clip = clip;
        if (!MenuManager.Instance.gameObject.activeSelf)
            music.Play();
    }
}
