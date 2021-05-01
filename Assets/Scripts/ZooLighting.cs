using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZooLighting : MonoBehaviour
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
        if (LightScript.ZooOn == false)
        {
            LightScript.level = 2;
            LightScript.Light();
        }
    }
}
