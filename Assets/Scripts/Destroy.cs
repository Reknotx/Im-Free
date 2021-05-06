using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public AudioClip musicClip;
    public AudioClip sfxClip;

    void Start()
    {

    }

    public void Delete()
    {
        Destroy(this.gameObject);
    }

}
