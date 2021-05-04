using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public AudioClip clip;

    void Start()
    {
        Camera.main.GetComponent<AudioSource>().clip = clip;
    }

    public void Delete()
    {
        Destroy(this.gameObject);
    }

}
