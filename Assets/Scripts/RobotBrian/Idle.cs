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


    private Vector3 GetRandomPosition()
    {
        Vector2 pos = Random.onUnitSphere * 2.5f;
        Vector3 calculatedPos = playerTransform.position + new Vector3(pos.x, .5f, pos.y);
        Vector3 direction = playerTransform.forward;

        calculatedPos += direction / 2;
        Vector3 wantedPos = calculatedPos;

        if (Physics.Raycast(calculatedPos, direction, out RaycastHit forwardHit, distanceFromPlayer))
        {
            wantedPos += direction * (forwardHit.distance * .75f);
            wantedPos = CheckDown(wantedPos);
        }
        else
        {
            wantedPos += direction * distanceFromPlayer;
            wantedPos = CheckDown(wantedPos);
        }

        return wantedPos;
    }

    private Vector3 CheckDown(Vector3 checkPos)
    {
        RaycastHit downHit;
        checkPos.y -= 0.1f;
        if (Physics.Raycast(checkPos, Vector3.down, out downHit, 10f))
        {
            return SampleHit(downHit.point);
        }
        return SampleHit(checkPos);
    }
    private Vector3 SampleHit(Vector3 checkPos)
    {
        NavMeshHit myNavHit;
        if (NavMesh.SamplePosition(checkPos, out myNavHit, 100f, NavMesh.AllAreas))
        {
            return myNavHit.position;
        }
        return checkPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(goalPos, 1f);
    }
}
