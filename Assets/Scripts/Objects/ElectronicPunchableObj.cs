using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicPunchableObj : PunchableObj
{
    public ParticleSystem sparks;

    private void Start()
    {
        if (sparks == null)
        {
            sparks = transform.GetChild(0).GetComponent<ParticleSystem>();
        }
    }


    public override void Punched()
    {
        if (BeenPunched) return;

        base.Punched();

        sparks.Play();
        StartCoroutine(SparksTimer());
    }

    IEnumerator SparksTimer()
    {
        yield return new WaitForSeconds(5f);
        sparks.Stop();
    }
}
