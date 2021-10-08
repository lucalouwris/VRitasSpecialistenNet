using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//Got from https://forum.unity.com/threads/solved-random-wander-ai-using-navmesh.327950/

public class Movement : MonoBehaviour
{
    public float walkRadius;
    public float newPositionTimer;
    public float peopleCount = 1;
    public float playTime;
    private System.Random rd = new System.Random();

    [SerializeField] private int duplicateMinTime = 3, duplicateMax = 10;

    private Transform target;
    private UnityEngine.AI.NavMeshAgent agent;
    private float timer;
    private float peopleTimer; 

    void Start()
    {
        
        walkRadius = rd.Next(5, 30);
        newPositionTimer = rd.Next(2, 10);
    }

    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        timer = newPositionTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        playTime += Time.deltaTime;
        if (gameObject.tag == "Original")
        {

            peopleTimer += Time.deltaTime;

            if (peopleTimer > rd.Next(duplicateMinTime,duplicateMax))
            {
                peopleTimer = 0;
                GameObject duplicate = Instantiate(GameObject.FindWithTag("Original"));
                duplicate.tag = "Untagged";
                peopleCount++;
            }
            }

        if (timer >= newPositionTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, walkRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;          
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        UnityEngine.AI.NavMeshHit navHit;

        UnityEngine.AI.NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void OnDestroy()
    {
        Debug.Log($"Player exited the map with {peopleCount} in the map, after {playTime}");
    }
}
