using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{

    [SerializeField] private DialogueObject dialogueObject;

    public void Interact(PlayerController player)
    {
        player.BrianSays.ShowDialogue(dialogueObject);
    }
}
