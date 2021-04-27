using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabLighting : LightManager
{
    private void OnTriggerEnter(Collider other)
    {
        level = 1;

        Light();
    }
}
