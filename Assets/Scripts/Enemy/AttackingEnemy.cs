﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingEnemy : Enemy
{
    public GameObject bullet;

    float shootDelay = 0.5f;
    float nextFire = 0.0f;


    private void Update()
    {

        if (!SeenPlayer) return;

        if (IsAttacked) return;
        //Ray ray = Physics.Raycast()

        //if ()

        if (Time.time > nextFire)
        {
            nextFire = Time.time + shootDelay;
            Shoot();
        }
    }


    public void Move()
    {

    }

    public void Shoot()
    {
        //get the current mouse position in 2D screen space
        Vector3 enemyPos = transform.position;

        //find delta from the launch pos to the mousePos3D
        Vector3 shootAngle = Player.Instance.transform.position - enemyPos;

        GameObject bull = Instantiate(bullet, transform.position, Quaternion.identity);

        bull.transform.LookAt(Player.Instance.gameObject.transform.position);
    }
}
