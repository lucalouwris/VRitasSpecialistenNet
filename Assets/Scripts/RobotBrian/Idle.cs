using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class Idle : BaseState
{
    private Vector3 goalPos = Vector3.zero;
    [SerializeField] private float floatHeight = 1f;
    [SerializeField] private float distanceFromPlayer = 4;

    //OnEnable is called when a transition starts and the state machine starts to evaluate this state
    public override void OnEnable()
    {
        base.OnEnable();
        goalPos = GetRandomPosition();
        navAgent.SetDestination(goalPos);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void Update()
    {
        // If far away move and rotate brian towards random position close to the player.
        if (Vector3.Distance(goalPos, transform.position) < 1f)
        {
            goalPos = GetRandomPosition();

            navAgent.SetDestination(goalPos);
        }
    }
}
