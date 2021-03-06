using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTimer : MonoBehaviour
{
    [SerializeField]private IngameTimer timer;
    
    /// <summary>
    /// Code that triggers when player opens the door.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // Checking if colliding object is the XR Rig
        if (other.CompareTag("Player") && timer.timerIsRunning == false)
        {
            timer.enabled = true;
            timer.Restart();
            gameObject.SetActive(false);
        }
    }
}
