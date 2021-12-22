using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BrianSays : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textlabel;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip sound;
    public static Action<string> brianSpeaking;


    private TypeWriter typeWriter;
    private bool isPressedRight = false;
    private bool isPressedLeft = false;

    public DialogueObject playThis;

    public bool isOpen { get; private set; }


    void OnEnable()
    {

        this.source.PlayOneShot(this.sound);
        this.CloseDialogeBox();
        this.typeWriter = GetComponent<TypeWriter>();
        ShowDialogue(playThis);
        brianSpeaking.Invoke(playThis.name);
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



            if(!this.typeWriter.isRunning)
            {
            yield return null;
            yield return new WaitUntil(() => this.isPressedRight == true && this.isPressedLeft == true);
            this.isPressedRight = false;
            this.isPressedLeft = false;
            }
        }

        this.CloseDialogeBox();
        this.gameObject.SetActive(false);
    }

    public void handlePressedRight()
    {
        this.isPressedRight = true;
    }
    public void handlePressedLeft()
    {
        this.isPressedLeft = true;
    }
    public void handleReleaseRight()
    {
        this.isPressedRight = false;
    }
    public void handleReleaseLeft()
    {
        this.isPressedLeft = false;
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        this.typeWriter.Run(dialogue, this.textlabel);

        while (this.typeWriter.isRunning)
        {
            yield return null;
        }

        this.typeWriter.Stop();
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
        brianSpeaking.Invoke("Walkin");
    }
}
