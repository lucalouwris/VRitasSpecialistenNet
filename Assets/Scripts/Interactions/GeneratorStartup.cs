using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorStartup : MonoBehaviour
{

    [SerializeField] private MeshRenderer objectRenderer;
    [SerializeField] private Material wantedMaterialPrimary;
    [SerializeField] private Material wantedMaterialSecondary;

    //Audio data
    [SerializeField] private AudioClip brokenClip;
    [SerializeField] private AudioClip fixedClip;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private GameObject firstStates;
    [SerializeField] private GameObject secondStates;
    [SerializeField] private AudioController audioController;

    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private StateMachine brian;
    [SerializeField] private BrianSays speaker;

    [SerializeField] private GameObject computerButton;

    //Refill oxygen
    [SerializeField] private PDAController oxygen;


    // Start is called before the first frame update
    void Start()
    {
        if (brokenClip != null)
        {
            audioSource.clip = brokenClip;
            audioSource.Play();
        }        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    //This method changes the material of the middle power
    public void activateGenerator()
    {
        Debug.Log("Updating Generator Materials");
        // get the current array of materials
        var materials = objectRenderer.materials;
        // exchange both materials with the activated material version // numbers are switched because unity is weird
        materials[1] = wantedMaterialSecondary;
        materials[2] = wantedMaterialPrimary;

        // reassign the materials to the renderer
        objectRenderer.materials = materials;

        // Changing the audio to fixed.
        audioController.PlayBackground();
        audioSource.Stop();
        audioSource.clip = fixedClip;
        audioSource.loop = false;
        audioSource.Play();

        this.renderNewStates();

        brian.SwitchState(brian.States[1]);
        this.speaker.playThis = dialogueObject;

        // Turn on computer for minigame 2
        this.computerButton.SetActive(true);

        //Refilling oxygen
        oxygen.fillOxygen();
    }

    public void renderNewStates()
    {
        firstStates.SetActive(false);
        secondStates.SetActive(true);
    }
}
