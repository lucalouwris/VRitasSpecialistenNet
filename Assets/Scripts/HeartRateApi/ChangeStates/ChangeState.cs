using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    [SerializeField] private GameStates stateEnum;
    [SerializeField] private GameStateEnum newState;

    private void OnTriggerEnter(Collider other)
    {     
        stateEnum.setGameState(newState);
    }
}
