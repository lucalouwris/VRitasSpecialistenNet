using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
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
        Debug.Log($"Tag: {other.tag}, timer running: {timer.timerIsRunning}");
        // Checking if colliding object is the XR Rig
        if (other.CompareTag("Player") && timer.timerIsRunning == false)
        {
            timer.enabled = true;
            timer.Restart();
            gameObject.SetActive(false);
        }
    }
}
