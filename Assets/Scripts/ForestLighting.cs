using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestLighting : MonoBehaviour
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
        LightScript.level = 4;
        LightScript.Light();
    }
}