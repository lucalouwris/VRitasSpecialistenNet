using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCheck : MonoBehaviour
{
    /// <summary>
    /// When certain task is completed, set this bool to true so when the player pulls the lever down it the switch works.
    /// </summary>
    public bool SwitchShouldWork;

    private void OnTriggerEnter(Collider other)
    {
        SwitchShouldWork = true;
    }
}
