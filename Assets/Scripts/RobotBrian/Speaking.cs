using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Speaking : BaseState
{
    
    [SerializeField] private float distanceFromPlayer = 1.4f;
    [SerializeField] private GameObject canvasObject;
    private Vector3 goalPos;
    private Vector3 offset;

    public override void Start()
    {
        base.Start();
        goalPos = GetRandomPosition();
        navAgent.SetDestination(goalPos);
    }

    // OnEnable is called when a transition starts and the state machine starts to evaluate this state
    public override void OnEnable()
    {
        base.OnEnable();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void Update()
    {
        if (Vector3.Distance(transform.position, goalPos) < 1f)
        {
            transform.LookAt(playerTransform);
            SpeakLine();
        }

        if (Vector3.Distance(playerTransform.position, goalPos) > 10)
        {
            goalPos = GetRandomPosition();
            navAgent.SetDestination(goalPos);
        }
    }

    private void SpeakLine()
    {
        if (!canvasObject.activeSelf)
        {
            Debug.Log("speakline active");
            canvasObject.SetActive(true);
        }
    }
    
    private Vector3 GetRandomPosition()
    {
        RaycastHit ForwardHit;
        //Vector2 pos = Random.onUnitSphere * 2.5f;
        Vector3 newPos = playerTransform.position; //new Vector3(pos.x, .5f, pos.y) + 
        Vector3 direction = new Vector3(playerTransform.forward.x, 0, playerTransform.forward.z).normalized;
        Debug.Log(newPos);

        newPos = newPos + direction * distanceFromPlayer;
        RaycastHit hit;
        if (Physics.Raycast(newPos, Vector3.down, out hit, 10f))
        {
            NavMeshHit myNavHit;
            if(NavMesh.SamplePosition(hit.point, out myNavHit, 100f,NavMesh.AllAreas))
            {
                newPos = myNavHit.position;
            }
        }
        
        
        

        return newPos;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnDisable()
    {
        canvasObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(goalPos, .5f);
    }
}
