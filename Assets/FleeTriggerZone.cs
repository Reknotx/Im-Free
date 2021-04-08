using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeTriggerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Spotted enemy: " + other.name);

        other.GetComponent<Enemy>().SeenPlayer = true;
    }
}
