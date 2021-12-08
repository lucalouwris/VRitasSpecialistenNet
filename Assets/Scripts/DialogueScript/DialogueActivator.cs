using UnityEngine;

public class DialogueActivator : MonoBehaviour
{

    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private StateMachine brian;
    [SerializeField] private BrianSays speaker;
    [SerializeField] private Vector3 position;

    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            
            if (!this.isTriggered) //State should only trigger once
            {
                brian.transform.position = position;
                Debug.Log("switchState");
                brian.SwitchState(brian.States[1]);

                this.isTriggered = true;
            }
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