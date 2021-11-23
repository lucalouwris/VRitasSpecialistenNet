using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BrianSays : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textlabel;
    [SerializeField] private DialogueObject[] dialogue;

    private TypeWriter typeWriter;
    private int count = 0;
    void OnEnable()
    {
        this.CloseDialogeBox();
        this.typeWriter = GetComponent<TypeWriter>();
        this.ShowDialogue(dialogue[count % dialogue.Length]);
        count ++;
    }


   public void ShowDialogue(DialogueObject dialogueObject)
    {
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
        }

        this.CloseDialogeBox();
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        this.typeWriter.Run(dialogue, this.textlabel);

        while (this.typeWriter.isRunning)
        {
            yield return null;
        }
    }

    private void CloseDialogeBox()
    {
        this.dialogueBox.SetActive(false);
        this.textlabel.text = string.Empty;
    }
}
