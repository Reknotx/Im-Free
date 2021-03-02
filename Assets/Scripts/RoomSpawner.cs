using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public List<GameObject> spawnableRooms = new List<GameObject>();
    public List<GameObject> spawnableCorridors = new List<GameObject>();


    public List<GameObject> spawnedObjs = new List<GameObject>();


    enum Spawning
    {
        room,
        corridor
    }

    private Spawning spawning = Spawning.room;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(spawnableRooms.Count + spawnableCorridors.Count);
        SpawnLevel();

    }


    void SpawnLevel()
    {
        List<GameObject> spawnOrder = new List<GameObject>();
        
        DetermineOrder();

        SpawnRooms();

        void DetermineOrder()
        {
            Debug.Log("Determining order");
            while (spawnOrder.Count < (spawnableCorridors.Count + spawnableRooms.Count))
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

                            if (!spawnOrder.Contains(spawnableCorridors[corridorIndex]))
                            {
                                spawnOrder.Add(spawnableCorridors[corridorIndex]);
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

            foreach (GameObject item in spawnOrder)
            {
                GameObject spawned = Instantiate(item);
                spawnedObjs.Add(spawned);
            }

            for (int i = 0; i < spawnedObjs.Count; i++)
            {
                GameObject spawned = (GameObject)spawnedObjs[i];

                Vector3 spawnPos;

                if (i == 0) spawnPos = Vector3.zero; 
                else spawnPos = spawnedObjs[i - 1].transform.Find("Room End").position;

                spawned.transform.position = spawnPos;

            }
        }

    }


    

    // Update is called once per frame
    void Update()
    {
        
    }
}
