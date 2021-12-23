using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class BaseState : MonoBehaviour
{
    public int TriggerChange = 0;
    public NavMeshAgent navAgent;
    public Transform playerTransform;
    public float distanceFromPlayer = 4;

    public virtual void Start()
    {
        //navAgent = GetComponent<NavMeshAgent>();
        //playerTransform = Camera.main.transform;
    }

    // OnEnable is called when a transition starts and the state machine starts to evaluate this state
    public virtual void OnEnable()
    {
        Debug.Log(gameObject.GetComponent<MonoBehaviour>());
    }

    // Update is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public virtual void Update()
    {
        
    }
    
    public Vector3 GetRandomPosition()
    {
        RaycastHit ForwardHit;
        //Vector2 pos = Random.onUnitSphere * 2.5f;
        Vector3 calculatedPos = playerTransform.position; //new Vector3(pos.x, .5f, pos.y) + 
        Vector3 direction = new Vector3(playerTransform.forward.x, 0, playerTransform.forward.z).normalized;

        calculatedPos += direction/2;
        Vector3 wantedPos = calculatedPos;
        
        if(Physics.Raycast(calculatedPos, direction, out ForwardHit, distanceFromPlayer))
        {
            wantedPos += direction * (ForwardHit.distance * .75f);
            wantedPos = CheckDown(wantedPos);
        }
        else
        {
            wantedPos += direction * distanceFromPlayer;
            wantedPos = CheckDown(wantedPos);
        }

        return wantedPos;
    }

    public Vector3 CheckDown(Vector3 checkPos)
    {
        RaycastHit downHit;
        if (Physics.Raycast(checkPos, Vector3.down, out downHit, 10f))
        {
            return SampleHit(downHit.point);
        }
        return SampleHit(checkPos);
    }
    public Vector3 SampleHit(Vector3 checkPos)
    {
        NavMeshHit myNavHit;
        if (NavMesh.SamplePosition(checkPos, out myNavHit, 1f, NavMesh.AllAreas))
        {
            return myNavHit.position;
        }
        return checkPos;
    }

    // OnDisable is called when a transition ends and the state machine finishes evaluating this state
    public virtual void OnDisable()
    {
        
    }
}