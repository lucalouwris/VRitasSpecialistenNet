using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState[] States;
    [SerializeField] private int StartState;
    [SerializeField] private BaseState currentState;
    [SerializeField] BrianPointer pointer;

    private void Start()
    {
        States[StartState].enabled = true;
        currentState = States[StartState];
    }

    public void SwitchState(BaseState newState)
    {
        if (newState == GetComponent<Idle>())
            pointer.isActive = false;
        else
            pointer.isActive = true;
        currentState.enabled = false;
        newState.enabled = true;
        currentState = newState;
    }
}
