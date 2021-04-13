using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    //private void OnParticleSystemStopped()
    //{
    //    Destroy(gameObject);
    //}

    private void Update()
    {
        if (GetComponent<ParticleSystem>().isStopped)   
        {
            Destroy(gameObject);
        }
    }
}
