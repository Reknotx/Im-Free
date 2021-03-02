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

        while(spawnOrder.Count < (spawnableRooms.Count + spawnableCorridors.Count))
        {
            bool findingSpawn = true;
            while(findingSpawn)
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
                    
                    default:
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
