using UnityEngine;

public class DialogueActivator : MonoBehaviour
{

    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private StateMachine brian;
    [SerializeField] private BrianSays speaker;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

            Debug.Log("switchState");
            brian.SwitchState(brian.States[1]);
            this.speaker.playThis = dialogueObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController player))
        {
      
        }

    }
}