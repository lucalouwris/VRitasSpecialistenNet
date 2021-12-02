using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{

    [SerializeField] private DialogueObject dialogueObject;

    public void Interact(PlayerController player)
    {
        GetComponent<StateMachine>().SwitchState(GetComponent<StateMachine>().States[1]);
        player.BrianSays.ShowDialogue(dialogueObject);
    }
}
