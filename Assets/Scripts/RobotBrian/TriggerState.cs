using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerState : MonoBehaviour
{
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private int wantedState;
    [SerializeField] private string TagCheck;

    [SerializeField] private string objectTag;
    [SerializeField] private bool shouldDisable;

    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(TagCheck))
        {

            if(this.tag == objectTag)
            {
                stateMachine.SwitchState(stateMachine.States[wantedState]);
                this.gameObject.SetActive(false);
            }
        }
    }
}
