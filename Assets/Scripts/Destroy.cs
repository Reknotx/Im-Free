using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public AudioClip musicClip;
    public AudioClip sfxClip;

    void Start()
    {
        AudioSources sources = new AudioSources();
        sources.musicClip = musicClip;
        sources.sfxClip = sfxClip;
        AudioManager.Instance.ChangeTracks(sources);
    }

    public void Delete()
    {
        Destroy(this.gameObject);
    }

}
