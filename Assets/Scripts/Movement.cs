using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Got from https://forum.unity.com/threads/solved-random-wander-ai-using-navmesh.327950/

public class Movement : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;
    public float peopleCount = 1;
    private System.Random rd = new System.Random();

    [SerializeField] private int duplicateMin = 3, duplicateMax = 10;
    //public GameObject rootObj;
    //private GameObject rootObj = GameObject.FindWithTag("Original");

    private Transform target;
    private UnityEngine.AI.NavMeshAgent agent;
    private float timer;
    private float peopleTimer; 

    void Start()
    {
        
        wanderRadius = rd.Next(5, 30);
        wanderTimer = rd.Next(2, 10);
    }

    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (gameObject.tag == "Original")
        {

            peopleTimer += Time.deltaTime;

            if (peopleTimer > rd.Next(duplicateMin,duplicateMax))
            {
                peopleTimer = 0;
                GameObject duplicate = Instantiate(GameObject.FindWithTag("Original"));
                duplicate.tag = "Untagged";         
            }
            }

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
            peopleCount++;           
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
}
