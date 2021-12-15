using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] Transform spawnPoints;
    private Transform[] spawnPointList;
    [SerializeField] private Transform alienParent;
    [SerializeField] private int amountToSpawn = 3;
    
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
    [ContextMenu("Spawn 3 aliens")]
    void SpawnFromInspector()
    {
        spawnAliens(amountToSpawn);
    }

    //call this method to spawn x amount of Aliens
    public void spawnAliens(int amountOfAliens)
    {
        if (amountOfAliens <= -1)
        {
            Debug.LogError("Very funny, trying to spawn " + amountOfAliens + " Alien(s)");
        }
        else
        {
            for (int i = 0; i < amountOfAliens; i++)
            {
                Debug.Log($"Spawn {i}");
                spawnObject(spawnPointList[i% spawnPointList.Length]);
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