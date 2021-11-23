using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState[] States;
    public bool[] Triggers;
    [SerializeField] private int StartState;
    [SerializeField] private BaseState currentState;

    private void Start()
    {
        States[StartState].enabled = true;
        currentState = States[StartState];
    }

    public void SwitchState(BaseState newState)
    {
        Triggers[newState.TriggerChange] = false;
        currentState.enabled = false;
        newState.enabled = true;
        currentState = newState;
    }

    private void Update()
    {
        foreach (BaseState state in States)
        {
            if (state != currentState && Triggers[state.TriggerChange])
            {
                Triggers[state.TriggerChange] = false;
                currentState.enabled = false;
                state.enabled = true;
                currentState = state;
            }
        }
    }
}
