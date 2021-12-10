using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerState : MonoBehaviour
{
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private int wantedState;
    [SerializeField] private string TagCheck;

    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(TagCheck))
        {
                stateMachine.SwitchState(stateMachine.States[wantedState]);
                this.gameObject.SetActive(false);
            
        }
    }
}
