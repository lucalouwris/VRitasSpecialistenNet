using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] Transform spawnPoints;
    private Transform[] spawnPointList;
    private bool thisistrue = true;
    // Start is called before the first frame update
    void Start()
    {
        //extracting all the spawnpoints into an array
        if (Object.ReferenceEquals(spawnPoints, null))
        {
            Debug.Log("No spawnPoints set in the inspector, what da heck!");
        }
        else
        {
            //getting all Transforms of spawnpoints
            spawnPointList = GetComponentsInChildren<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (thisistrue)
        {
            spawnAliens(3);
            thisistrue = false;
        }



    }

    //call this method to spawn x amount of Aliens
    public void spawnAliens(int amountOfAliens)
    {
        int aliensspawned = 0;

        if (amountOfAliens <= -1)
        {
            Debug.Log("Very funny, trying to spawn " + amountOfAliens + " Alien(s)");
        }
        else
        {
            // for the amount of aliens loop through the array step by step and spawn an alien at every spawnpoint
            while (aliensspawned != amountOfAliens)
            {
                foreach (Transform t in spawnPointList)
                {
                    if (aliensspawned == amountOfAliens)
                    {
                        return;
                    }
                    else
                    {
                        spawnObject(t);
                        Debug.Log("Spawning Alien for " + t);
                        aliensspawned++;
                    }
                    
                }
            }

        }

    }
    void spawnObject(Transform spawnPoint)
    {
        Debug.Log("Trying to Spawn an " + objectToSpawn + " @ " + spawnPoint);
        Instantiate(objectToSpawn, spawnPoint);
        //spawningEnabled = false
    }

}