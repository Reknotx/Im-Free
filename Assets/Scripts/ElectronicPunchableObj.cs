using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicPunchableObj : PunchableObj
{
    public ParticleSystem sparks;


    public override void Punched()
    {
        if (BeenPunched) return;

        base.Punched();

        sparks.Play();
    }
}
