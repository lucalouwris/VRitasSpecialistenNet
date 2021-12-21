/*
    The DialogueActivator is used  to call new pieces of dialogue for Brian.
    It checks when the player or any other object with a specific tag has entered a trigger.
    If so the trigger gets removed so it only happens once and Brian gets new dialogue assigned to him.
    The values are assigned in the inspector on the gameObject.
*/
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{

    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private StateMachine brian;
    [SerializeField] private BrianSays speaker;
    [SerializeField] private Vector3 position;
    [SerializeField] private bool shouldDisable = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Lever") || other.CompareTag("brokenPiece"))
        {
            // If tag of the object entering the trigger collision has is true for either.
            //brian.transform.position = position; // Move Brian to the correct dialogue position.
            Debug.Log("switchState");
            brian.SwitchState(brian.States[1]); // Brian switches to the dialogue state.
            if (shouldDisable) // Only do this to disable the gameObject that contains the trigger collider.
            {
                this.gameObject.SetActive(false);
            }
            this.speaker.playThis = dialogueObject; // Brian speaks the specific dialogue.
        }
    }
}