using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabLighting : MonoBehaviour
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
        if (LightScript.LabOn == false)
        {
            LightScript.level = 1;
            LightScript.Light();
        }
    }
}
