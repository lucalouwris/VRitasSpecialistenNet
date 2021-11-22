using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BrianSays : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textlabel;
    [SerializeField] private DialogueObject testDialogue;

    private TypeWriter typeWriter;

    void Start()
    {
        this.CloseDialogeBox();
        this.typeWriter = GetComponent<TypeWriter>();
        this.ShowDialogue(testDialogue);

    }


   public void ShowDialogue(DialogueObject dialogueObject)
    {
        this.dialogueBox.SetActive(true);
        StartCoroutine(this.StepThroughDialogue(dialogueObject));
    }

    public IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach(string dialogue in dialogueObject.Dialogue)
        {
            yield return this.typeWriter.Run(dialogue, this.textlabel);

        }

        yield return new WaitForSeconds(1);
        this.CloseDialogeBox();
    }

    private void CloseDialogeBox()
    {
        this.dialogueBox.SetActive(false);
        this.textlabel.text = string.Empty;
    }
}
