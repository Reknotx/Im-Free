using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    /// <summary> Flag to check if the enemy has ever seen the player. </summary>
    public bool SeenPlayer { get; set; } = false;

    public void FixedUpdate()
    {
        if (!SeenPlayer) return;

        Flee();
    }

    public void Flee()
    {
        Vector3 heading = Player.Instance.transform.position - transform.position;

        //heading.y = 1f;

        heading *= -1;

        GetComponent<Rigidbody>().MovePosition(transform.position + heading.normalized * 5f * Time.deltaTime);
    }


}
