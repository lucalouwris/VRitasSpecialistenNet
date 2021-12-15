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

    [SerializeField] private SpawningManager spawningManager;

    [SerializeField] private int countOfAliens = 1;
    

    private void OnTriggerEnter(Collider other)
    {

        ui = GameObject.Find("ComputerMG2").GetComponent<ComputerUI>();
        if (gameObject.name == "Refuel")
        {
            ui.Wipe();
            brian.SwitchState(brian.States[1]);
            this.speaker.playThis = dialogueObject;
        }
        else if (gameObject.name == "OnButton")
        {
            ui.TurnOn();
            brian.SwitchState(brian.States[1]);
            this.speaker.playThis = dialogueObject;
            this.spawningManager.spawnAliens(countOfAliens);
        }
    }
}
