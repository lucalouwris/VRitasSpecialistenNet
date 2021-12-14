/*
    This script is for the ComputerUI where there are 2 main buttons to press.
    The first button you can press is the OnButton which turns the computer on.
    The second button starts the refueling process and destroys all notifications.
    It then ends by calling a dialogue with Brian.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseOverNotif : MonoBehaviour
{
    ComputerUI ui;

    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private StateMachine brian;
    [SerializeField] private BrianSays speaker;
    private void OnTriggerEnter(Collider other)
    {

        ui = GameObject.Find("ComputerMG2").GetComponent<ComputerUI>(); // Get the script component of the computer.
        if (gameObject.name == "Refuel") // The button to wipe the notifications and start rtefueling.
        {
            ui.Wipe();
            brian.SwitchState(brian.States[1]);
            this.speaker.playThis = dialogueObject;
        }
        else if (gameObject.name == "OnButton") // The button to turn the computer on.
        {
            ui.TurnOn();
            brian.SwitchState(brian.States[1]);
            this.speaker.playThis = dialogueObject;
        }
    }
}
