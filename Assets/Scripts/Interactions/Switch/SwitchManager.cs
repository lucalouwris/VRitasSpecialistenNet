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

    private void Update()
    {
        Debug.Log(switchJoint.limits.max - switchJoint.angle);
        if (switchJoint.limits.max - switchJoint.angle < 10)
        {
            OnSwitchDown();
        }
    }

    private void OnSwitchDown()
    {
        if (ShouldCheckObject)
        {
            taskCompleted = ObjectCheck.GetComponent<SwitchCheck>().SwitchShouldWork;
        }
        else
        {
            taskCompleted = true;
        }

        if (taskCompleted)
        {
            TriggerObject();
        }
    }

    private void TriggerObject()
    {
        ObjectAnimator.SetTrigger(animatorTrigger);
        enabled = false;
    }
}
