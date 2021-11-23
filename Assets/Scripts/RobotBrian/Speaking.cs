using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaking : BaseState
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float distanceFromPlayer = 1.4f;
    [SerializeField] private GameObject canvasObject;
    private Vector3 goalPos;
    private Vector3 offset;

    // OnEnable is called when a transition starts and the state machine starts to evaluate this state
    public override void OnEnable()
    {
        base.OnEnable();
        offset = Vector3.right + Vector3.up * .4f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void Update()
    {
        //Calculating preferred position.
        goalPos = playerTransform.position + playerTransform.forward * distanceFromPlayer + offset;
        transform.position = Vector3.MoveTowards(transform.position, goalPos, .5f * Time.deltaTime);

        if (Vector3.Distance(transform.position, goalPos) < .4f)
        {
            SpeakLine();
        }
    }

    private void SpeakLine()
    {
        canvasObject.SetActive(true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnDisable()
    {
        canvasObject.SetActive(false);
    }
}
