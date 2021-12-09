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
        if(Vector3.Distance(goalPos, transform.position) < .5f)
        {
            Debug.Log("Reached position");
            goalPos = GetRandomPosition();
            
            navAgent.SetDestination(goalPos);
        }
        
        // If close by make sure it's around the vision of the player.
        
    }

    private Vector3 GetRandomPosition()
    {
        Vector2 pos = Random.onUnitSphere * 2.5f;
        Vector3 newPos = new Vector3(pos.x, 0, pos.y);
        Debug.Log($"Base random {newPos}");
        newPos += playerTransform.position + playerTransform.forward * Random.Range(3f, 5f);
        Debug.Log($"PlayerRandom {newPos}");

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(newPos, Vector3.down, out hit, Mathf.Infinity))
        {
            return hit.point;
        }
        return newPos;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(goalPos,1f);
    }
}
