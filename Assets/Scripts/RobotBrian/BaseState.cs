using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : MonoBehaviour
{
    public int TriggerChange = 0;

    [SerializeField] private PlayerInfo playerInfo;
    
    // OnEnable is called when a transition starts and the state machine starts to evaluate this state
    public virtual void OnEnable()
    {
        Debug.Log(this.gameObject.GetComponent<MonoBehaviour>());
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