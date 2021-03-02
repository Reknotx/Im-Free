using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public List<GameObject> spawnableRooms = new List<GameObject>();
    public List<GameObject> spawnableCorridors = new List<GameObject>();

    public List<GameObject> spawnOrder = new List<GameObject>();


    enum Spawning
    {
        room,
        corridor
    }

    private Spawning spawning = Spawning.room;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnLevel();

        Debug.Log(spawnableRooms.Count + spawnableCorridors.Count);
    }


    void SpawnLevel()
    {
        DetermineOrder();

        SpawnRooms()
    }


    void DetermineOrder()
    {
        Debug.Log("Determining order");
        while (spawnOrder.Count < 4)
        {
            bool findingSpawn = true;
            while (findingSpawn)
            {

                switch (spawning)
                {
                    case Spawning.room:
                        int roomIndex = Random.Range(0, spawnableRooms.Count);

                        if (!spawnOrder.Contains(spawnableRooms[roomIndex]))
                        {
                            spawnOrder.Add(spawnableRooms[roomIndex]);
                            spawning = Spawning.corridor;
                            findingSpawn = false;
                        }

                        break;

                    case Spawning.corridor:
                        int corridorIndex = Random.Range(0, spawnableCorridors.Count);

                        if (!spawnOrder.Contains(spawnableRooms[corridorIndex]))
                        {
                            spawnOrder.Add(spawnableRooms[corridorIndex]);
                            spawning = Spawning.room;
                            findingSpawn = false;
                        }
                        break;
                }
            }
        }
    }

    void SpawnRooms()
    {
        Debug.Log("Spawning rooms");
        for (int i = 0; i < spawnOrder.Count; i++)
        {
            GameObject spawnable = (GameObject)spawnOrder[i];

            GameObject spawned = Instantiate(spawnable);

            Vector3 spawnPos;

            if (i == 0) spawnPos = Vector3.zero;
            else spawnPos = spawnOrder[i - 1].transform.Find("Room End").position;

            spawned.transform.position = spawnPos;

            spawnOrder.Add(spawned);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
