using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryLighting : MonoBehaviour
{
    GameObject LightM;
    LightManager LightScript;
    GameObject player;

    private void Start()
    {
        LightM = GameObject.FindWithTag("LightManager");
        LightScript = LightM.GetComponent<LightManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (LightScript.MilitaryOn == false)
        {
            LightScript.level = 3;
            LightScript.Light();
        }
        */

        if (other.gameObject == player)
        {
            LightScript.level = 3;
            LightScript.Light();
        }
    }
}
