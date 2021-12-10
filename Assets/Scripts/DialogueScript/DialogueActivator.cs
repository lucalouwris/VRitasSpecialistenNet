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
            brian.transform.position = position;
            Debug.Log("switchState");
            brian.SwitchState(brian.States[1]);
            if (shouldDisable)
            {
                this.gameObject.SetActive(false);
            }


            this.speaker.playThis = dialogueObject;
        }
    }
}