using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public AudioClip musicClip;
    public AudioClip sfxClip;

    public GameObject location;
    public CanvasGroup locationFade;

    void Start()
    {
        AmbientAudioManager.Instance.ChangeMusic(musicClip);

        location.gameObject.SetActive(true);

        LeanTween.alphaCanvas(locationFade, 1f, 1f).setOnComplete(StartDelay);
    }

    public void Delete()
    {
        Destroy(this.gameObject);
    }

    void StartDelay()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);


        LeanTween.alphaCanvas(locationFade, 0f, 1f);
    }
}
