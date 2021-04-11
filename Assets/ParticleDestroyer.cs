using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        Destroy(gameObject);
    }
}
