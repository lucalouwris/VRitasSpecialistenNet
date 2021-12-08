using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BrianSays : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textlabel;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip sound;


    private TypeWriter typeWriter;
    private bool isPressed = false;

    public DialogueObject playThis;

    public bool isOpen { get; private set; }


    void OnEnable()
    {

        this.source.PlayOneShot(this.sound);
        this.CloseDialogeBox();
        this.typeWriter = GetComponent<TypeWriter>();
        ShowDialogue(playThis);
    }


   public void ShowDialogue(DialogueObject dialogueObject)
    {
        this.isOpen = true;
        this.dialogueBox.SetActive(true);
        StartCoroutine(this.StepThroughDialogue(dialogueObject));
    }

   public IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
     for(int i = 0;  i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];

            yield return this.RunTypingEffect(dialogue);

            this.textlabel.text = dialogue;


            yield return null;
            yield return new WaitUntil(() => this.isPressed == true);
            this.isPressed = false;
        }

        this.CloseDialogeBox();
        this.gameObject.SetActive(false);
    }

    public void handlePressed()
    {
        this.isPressed = true;
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        this.typeWriter.Run(dialogue, this.textlabel);

        while (this.typeWriter.isRunning)
        {
            yield return null;

            if(this.isPressed)
            {
                this.typeWriter.Stop();
            }
        }
    }

    public void CloseDialogeBox()
    {
        this.isOpen = false;
        this.dialogueBox.SetActive(false);
        this.textlabel.text = string.Empty;
    }

    private void OnDisable()
    {
        typeWriter.Stop();
        stateMachine.SwitchState(stateMachine.States[0]);
    }
}
