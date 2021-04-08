using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> The enemy class. </summary>
/// <remarks>Handles all enemy logic, primarily the enemy running away from the player.</remarks>
public class Enemy : MonoBehaviour
{
    /// <summary> Flag to check if the enemy has ever been attacked by the player. </summary>
    /// <value>A value of true means the enemy has been attacked.</value>
    public bool IsAttacked{ get; set; } = false;
    /// <summary> Flag to check if the enemy has ever seen the player. </summary>
    /// <value>A value of true means the enemy has seen the player and will now flee/attack. </value>
    public bool SeenPlayer { get; set; } = false;
}
