using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Speaking : BaseState
{
    [SerializeField] private GameObject canvasObject;
    private Vector3 goalPos;
    private Vector3 offset;

    // OnEnable is called when a transition starts and the state machine starts to evaluate this state
    public override void OnEnable()
    {
        base.OnEnable();
        
        goalPos = GetRandomPosition();
        navAgent.SetDestination(goalPos);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, 5f));
        if (Vector3.Distance(transform.position, goalPos) < 1f)
        {
            transform.LookAt(playerTransform);
            SpeakLine();
        }

        if (Vector3.Distance(playerTransform.position, transform.position) > distanceFromPlayer)
        {
            goalPos = GetRandomPosition();
            navAgent.SetDestination(goalPos);
        }
    }

    private void SpeakLine()
    {
        if (!canvasObject.activeSelf)
        {
            canvasObject.SetActive(true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnDisable()
    {
        canvasObject.SetActive(false);
    }
}
