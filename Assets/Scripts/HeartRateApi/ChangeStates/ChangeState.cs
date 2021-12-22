using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    [SerializeField] private GameStates stateEnum;    
    [SerializeField] private BaseState newState;

    private void OnTriggerEnter(Collider other)
    {     
        stateEnum.gameState(newState.name);
    }
}
