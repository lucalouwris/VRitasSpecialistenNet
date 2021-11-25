using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class Idle : BaseState
{
    private Vector3 goalPos = Vector3.zero;
    [SerializeField] private float floatHeight = 1f;

    [SerializeField] private Transform playerTransform;
    
    //OnEnable is called when a transition starts and the state machine starts to evaluate this state
    public override void OnEnable()
    {
        base.OnEnable();
        goalPos = transform.position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void Update()
    {
        // If far away move and rotate brian towards random position close to the player.

        if(Vector3.Distance(goalPos, transform.position) < .3f)
        {
            Vector2 pos = Random.onUnitSphere * 2.5f;
            goalPos = new Vector3(pos.x, floatHeight, pos.y);
            goalPos += playerTransform.position + playerTransform.forward * Random.Range(3f,5f);
        }
        transform.position = Vector3.MoveTowards(transform.position, goalPos, .5f * Time.deltaTime);
        // If close by make sure it's around the vision of the player.
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // public override void OnDisable()
    // {
    //     
    // }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position - playerTransform.position,1f);
    }
}
