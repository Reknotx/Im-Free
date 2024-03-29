﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeingEnemy : Enemy
{
    [SerializeField]
    private AudioClip fleeingClip;

    public void FixedUpdate()
    {
        if (!SeenPlayer) return;

        if (IsAttacked) return;

        Flee();
    }

    /// Author: Chase O'Connor
    /// Date: 2/10/2021
    /// <summary> Causes the enemy to flee in the opposite direction of the player. </summary>
    public void Flee()
    {
        Vector3 heading = Player.Instance.transform.position - transform.position;

        heading *= -1;

        GetComponent<Rigidbody>().MovePosition(transform.position + heading.normalized * 5f * Time.deltaTime);
        //transform.LookAt(heading);
    }

    public override bool SeenPlayer 
    { 
        get => base.SeenPlayer; 
        set
        {
            if (!base.SeenPlayer) deathSound.PlayOneShot(fleeingClip);
            base.SeenPlayer = value;
            animController.SetTrigger("CastRun");
        }
    }
}
