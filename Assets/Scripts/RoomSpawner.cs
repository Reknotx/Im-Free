using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public FloorInfo levels;

    bool beenAroundBefore = false;
    enum Spawning
    {
        room,
        corridor
    }

    private int index = 0;

    public Transform lastEndPoint = null;

    private Spawning spawning = Spawning.room;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnLevel();
    }


    private void FixedUpdate()
    {
        if (lastEndPoint != null && Vector3.Distance(Player.Instance.transform.position, lastEndPoint.position) < 15)
        {
            Debug.Log("Success");
            SpawnNext();
        }
    }


    void SpawnLevel()
    {
        List<GameObject> spawnOrder = new List<GameObject>();
        
        DetermineOrder();

        SpawnRooms();

        void DetermineOrder()
        {
            Debug.Log("Determining order");

            ///Determine the order of ALL themes right at the start
            ///increase the index until we have gone through all the levels

            List<GameObject> Rooms = levels.floorInfo[index].rooms;
            List<GameObject> Corridors = levels.floorInfo[index].corridorRooms;

            while (spawnOrder.Count < (Rooms.Count + (levels.RCCount * 5)))
            {
                bool findingSpawn = true;

                while (findingSpawn)
                {
                    switch (spawning)
                    {
                        case Spawning.room:

                            if (levels.floorInfo[index].levelName == "Lab" && spawnOrder.Count == 0 && !beenAroundBefore)
                            {
                                spawnOrder.Add(Rooms[0]);
                                spawning = Spawning.corridor;
                                findingSpawn = false;

                            }
                            else /*(!spawnOrder.Contains(Rooms[roomIndex]))*/
                            {
                                int roomIndex = Random.Range(1, Rooms.Count);
                                spawnOrder.Add(Rooms[roomIndex]);
                                spawning = Spawning.corridor;
                                findingSpawn = false;
                            }

                            break;

                        case Spawning.corridor:
                            for (int i = 0; i < 5; i++)
                            {
                                int corridorIndex = Random.Range(0, Corridors.Count);
                                spawnOrder.Add(Corridors[corridorIndex]);
                            }
                            spawning = Spawning.room;
                            findingSpawn = false;
                            break;
                    }
                }
            }
        }

        void SpawnRooms()
        {
            Debug.Log("Spawning rooms");
            List<GameObject> spawnedObjs = new List<GameObject>();

            foreach (GameObject item in spawnOrder)
            {
                GameObject spawned = Instantiate(item);
                spawnedObjs.Add(spawned);
            }

            for (int i = 0; i < spawnedObjs.Count; i++)
            {
                GameObject spawned = (GameObject)spawnedObjs[i];

                Vector3 spawnPos;



                if (lastEndPoint == null)
                {
                    //lastEndPoint = spawned.transform.Find("Room End");
                    spawnPos = Vector3.zero;
                }
                else
                {
                    //if (i - 1 != -1) lastEndPoint = spawnedObjs[i - 1].transform.Find("Room End");

                    spawnPos = lastEndPoint.position;
                }

                lastEndPoint = spawned.transform.Find("Room End");

                spawned.transform.position = spawnPos;

            }
        }

    }


    public void SpawnNext()
    {
        beenAroundBefore = true;
        index++;
        if (index >= levels.floorInfo.Count)
        {
            index = 0;
        }

        SpawnLevel();
    }

}
