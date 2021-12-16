/*
    The SwitchManager is a script for the lever interaction. 
    What it does is trigger objects like the hatch of the spaceship and the startup of the generator.
    For the generator the lever must be able to return to its up rotation when the generator minigame has not been completed yet.
    If it has then it runs the TriggerObject() function.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    [SerializeField] private HingeJoint switchJoint;
    [SerializeField] private bool ShouldCheckObject;
    [SerializeField] private GameObject ObjectCheck;
    private bool taskCompleted;
    [SerializeField] private Animator ObjectAnimator;
    public bool shouldUseAnimation;
    
    //Audio data
    [SerializeField] private AudioClip leverClick;
    [SerializeField] private AudioSource lever;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource audioSource;
    private bool leverDown = false;
    private bool hatchDown = false;

    [SerializeField] ComputerUI minigameTwo;
    [SerializeField] string leverType;

    private void Update()
    {
        // Checking the angle to see if the switch code should run 
        if (switchJoint.limits.max - switchJoint.angle < 10)
        {
            if (!leverDown)
            {
                OnSwitchDown(); 
                lever.PlayOneShot(leverClick);
            }
            leverDown = true;
        }
        else
            leverDown = false;
        if (switchJoint.angle < 2)
            switchJoint.useSpring = false;
    }

    private void OnSwitchDown()
    {
        // If there is an object linked to the switch, that should have its state set to true.
        if (ShouldCheckObject)
            taskCompleted = ObjectCheck.GetComponent<SwitchCheck>().SwitchShouldWork;
        else
            taskCompleted = true;
        // If its completed, run task and make sure it doesn't spring back. Else it should spring back to make clear it didn't work.
        if (taskCompleted && leverType == "Generator")
        {
            switchJoint.useSpring = false;
            TriggerGenerator();
        }
        else if (leverType == "Hatch" && minigameTwo != null)
        {
            switchJoint.useSpring = true;
            TriggerHatch();
        }
        else
            switchJoint.useSpring = true;
    }

    private void TriggerGenerator()
    {
        GetComponent<GeneratorStartup>().activateGenerator();

        if (clip != null) // If there is a sound to be played.
            audioSource.PlayOneShot(clip);
        enabled = false;
    }
    private void TriggerHatch()
    {
        if (!hatchDown)
        {
            ObjectAnimator.ResetTrigger("DoorCloses");
            ObjectAnimator.SetTrigger("DoorOpens");
            hatchDown = true;
        }
        else if(hatchDown && minigameTwo.completed)
        {
            ObjectAnimator.ResetTrigger("DoorOpens");
            ObjectAnimator.SetTrigger("DoorCloses");
            hatchDown = false;
        }

        if (clip != null) // If there is a sound to be played.
            audioSource.PlayOneShot(clip);
    }
}
