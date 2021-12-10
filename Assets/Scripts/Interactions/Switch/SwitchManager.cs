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
    [SerializeField] private string animatorTrigger;
    public bool shouldUseAnimation;
    
    //Audio data
    [SerializeField] private AudioClip leverClick;
    [SerializeField] private AudioSource lever;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource audioSource;
    private bool leverDown =false;

    /// <summary>
    /// Checking the angle to see if the switch code should run 
    /// </summary>
    private void Update()
    {
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
        {
            leverDown = false;
        }
        if (switchJoint.angle < 2)
        {
            switchJoint.useSpring = false;
        }
    }

    private void OnSwitchDown()
    {
        // If there is an object linked to the switch, that should have its state set to true
        if (ShouldCheckObject)
        {
            taskCompleted = ObjectCheck.GetComponent<SwitchCheck>().SwitchShouldWork;
        }
        else
        {
            taskCompleted = true;
        }


        
        
        // If its completed, run task and make sure it doesn't spring back. Else it should spring back to make clear it didn't work.
        if (taskCompleted)
        {
            switchJoint.useSpring = false;
            TriggerObject();
        }
        else
        {
            switchJoint.useSpring = true;
        }
    }

    private void TriggerObject()
    {
        if (shouldUseAnimation)
        {
            ObjectAnimator.SetTrigger(animatorTrigger);
        }
        else
        {
           GetComponent<GeneratorStartup>().activateGenerator();
        }

        if (clip != null)
        {            
            audioSource.PlayOneShot(clip);
        }        
        enabled = false;
    }
}
