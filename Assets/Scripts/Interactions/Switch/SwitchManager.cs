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

    private void Update()
    {
        if (switchJoint.limits.max == switchJoint.angle)
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
        
    }
}
