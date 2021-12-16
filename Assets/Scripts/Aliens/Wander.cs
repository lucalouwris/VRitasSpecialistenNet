/*
    Every time the timer goes to zero the script and the player is not within range, it calculates a new point within a certain radius in a public static Vector3 called RandomNavSphere. 
    The new point is chosen with Random.insideUnitSphere which returns a random point inside or on a sphere with a 1.0 radius which we need to multiply with the value of dist. 
    It then marks the point as a NavMeshHit and returns the hit back which gets used to set a new destination with agent.SetDestination. 
    If the player is in range of the alien, the alien does nothing else but look at the direction of the player on the Y axis. 
    The radius, timer and the range of the alien can be set in the Inspector.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    [SerializeField] float wanderRadius;
    float wanderTimer;
    [SerializeField] float lookingDistance;
    GameObject player;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;
    bool isLooking = false;
    float speed = 2.0f;

    // Use this for initialization
    void OnEnable()
    {
        player = GameObject.Find("XR Rig");
        agent = GetComponent<NavMeshAgent>();
        wanderTimer = Random.Range(2,6);
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < wanderTimer)
            timer += Time.deltaTime;


        if (Vector3.Distance(transform.position, player.transform.position) < lookingDistance) // If the player is in range of the alien. Look at player.
        {
            if (!isLooking)
            {
                agent.SetDestination(transform.position);
                isLooking = true;
            }
            Vector3 relativePos = player.transform.position - transform.position;
            float singleStep = speed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, relativePos, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > lookingDistance && timer >= wanderTimer) // If the player is not in range of the alien. Walk around.
        {
            wanderTimer = Random.Range(2,6);
            isLooking = false;
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) // Define new point to set a new destination for agent.
    {
        Vector3 randDirection = Random.insideUnitSphere * dist; // Random point in or on the sphere.

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask); // Finds the nearest point based on the NavMesh within a specified range.

        return navHit.position; // Return to Vector3 newPos.
    }
}
