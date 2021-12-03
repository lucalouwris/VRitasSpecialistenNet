using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{

    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private GameObject brian;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && other.TryGetComponent(out PlayerController player))
        {
            player.Interactable = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController player))
        {
           if(player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }
        }

    }

    public void Interact(PlayerController player)
    {
        brian.GetComponent<StateMachine>().SwitchState(GetComponent<StateMachine>().States[1]);
        player.BrianSays.ShowDialogue(dialogueObject);
    }
}
