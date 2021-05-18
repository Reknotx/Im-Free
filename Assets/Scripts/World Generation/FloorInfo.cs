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

    [Tooltip("The list of room prefabs that are used in corridors for this level theme.")]
    /// <summary> The list of room prefabs that are used in corridors for this level theme. </summary>
    public List<GameObject> corridorRooms;
}


[CreateAssetMenu]
public class FloorInfo : ScriptableObject
{
    [Tooltip("The amount of rooms and corridors that are spawned.")]
    /// <summary> The amount of rooms and corridors that are spawned. </summary>
    public int RCCount = 3;

    public List<LevelStuff> floorInfo = new List<LevelStuff>();

    public void GenerateCorridors(int index)
    {
        
    }
}
