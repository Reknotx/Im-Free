using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct LevelStuff
{
    /// <summary> The name of this level theme. </summary>
    public string levelName;

    [Tooltip("The list of room prefabs for this level theme.")]
    /// <summary> The list of room prefabs for this level theme. </summary>
    public List<GameObject> rooms;

    [Tooltip("The list of corridor prefabs for this level theme.")]
    /// <summary> The list of corridor prefabs for this level theme. </summary>
    public List<GameObject> corridors;
}


[CreateAssetMenu]
public class FloorInfo : ScriptableObject
{
    public List<LevelStuff> floorInfo = new List<LevelStuff>();
}
