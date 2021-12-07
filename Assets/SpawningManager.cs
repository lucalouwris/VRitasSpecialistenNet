using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] Transform spawnPoint;

    private bool spawningEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        spawnObject();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void spawnObject()
    {
        if(spawningEnabled)
        {
        Debug.Log("Trying to Spawn an " + objectToSpawn + " @ " + spawnPoint);
        Instantiate(objectToSpawn, spawnPoint);
        spawningEnabled = false;
        }
        
    }
}
