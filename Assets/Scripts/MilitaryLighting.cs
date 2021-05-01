using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryLighting : MonoBehaviour
{
    GameObject LightM;
    LightManager LightScript;

    private void Start()
    {
        LightM = GameObject.FindWithTag("LightManager");
        LightScript = LightM.GetComponent<LightManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LightScript.MilitaryOn == false)
        {
            LightScript.level = 3;
            LightScript.Light();
        }
    }
}
