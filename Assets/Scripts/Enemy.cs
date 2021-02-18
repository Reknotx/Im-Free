using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> The enemy class. </summary>
/// <remarks>Handles all enemy logic, primarily the enemy running away from the player.</remarks>
public class Enemy : MonoBehaviour
{
    /// <summary> Flag to check if the enemy has ever seen the player. </summary>
    public bool SeenPlayer { get; set; } = false;

    public void FixedUpdate()
    {
        if (!SeenPlayer) return;

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
    }
}
