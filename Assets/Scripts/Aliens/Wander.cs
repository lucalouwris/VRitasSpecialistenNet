using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    [SerializeField] float wanderRadius;
    [SerializeField] float wanderTimer;
    [SerializeField] float lookingDistance;
    GameObject player;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;
    bool isLooking = false;

    // Use this for initialization
    void OnEnable()
    {
        player = GameObject.Find("XR Rig");
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < wanderTimer)
            timer += Time.deltaTime;

        
            if(Vector3.Distance(transform.position, player.transform.position) < lookingDistance)
            {
                if(!isLooking)
                    agent.SetDestination(transform.position);
                Vector3 relativePos = player.transform.position - transform.position;
                Quaternion LookAtRotation = Quaternion.LookRotation(relativePos);
                Quaternion LookAtRotationY = Quaternion.Euler(transform.rotation.eulerAngles.x, LookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                transform.rotation = LookAtRotationY;
            }
            else if (Vector3.Distance(transform.position, player.transform.position) > lookingDistance && timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
