using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeTriggerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 9) return;

        Debug.Log("Spotted enemy: " + other.name);

        other.GetComponent<FleeingEnemy>().SeenPlayer = true;
    }
}
