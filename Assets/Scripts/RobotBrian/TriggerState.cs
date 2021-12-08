using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerState : MonoBehaviour
{
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private int wantedState;
    [SerializeField] private string TagCheck;
    [SerializeField] private bool shouldDisable;

    private bool isTriggered = false;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagCheck))
        {
            if(!this.isTriggered)
            {
                stateMachine.SwitchState(stateMachine.States[wantedState]);
                this.isTriggered = true;

            }
        }
    }
}
