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
            Vector2 pos = Random.insideUnitCircle * 2f;
            goalPos = new Vector3(pos.x, floatHeight, pos.y);
            goalPos += playerTransform.position;
            Debug.Log(goalPos);
        }
        transform.position = Vector3.MoveTowards(transform.position, goalPos, .5f * Time.deltaTime);
        // If close by make sure it's around the vision of the player.
        
        Vector3[] frustumCorners = new Vector3[4];
        Camera cam = playerTransform.gameObject.GetComponent<Camera>();
        cam.CalculateFrustumCorners(new Rect(0, 0, 1, 1), cam.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);
        
        for (int i = 0; i < 4; i++)
        {
            var worldSpaceCorner = cam.transform.TransformVector(frustumCorners[i]);
            Debug.DrawRay(cam.transform.position, worldSpaceCorner, Color.blue);
        }
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
