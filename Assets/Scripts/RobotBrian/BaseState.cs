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

    // OnDisable is called when a transition ends and the state machine finishes evaluating this state
    public virtual void OnDisable()
    {
        
    }
}