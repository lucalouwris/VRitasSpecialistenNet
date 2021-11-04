using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool timerStarted = false;
    [SerializeField] public float timerLength = 12000;
    
    /// <summary>
    /// Code that runs every frame.
    /// </summary>
    private void Update()
    {
        // Simple count down timer
        if (timerStarted)
        {
            timerLength -= Time.deltaTime;
        }
        
        // Checking if timer ran out
        if (timerLength < 0)
        {
            Debug.Log("Time is up.");
            timerLength = 0;
            timerStarted = false;
        }
    }

    /// <summary>
    /// Code that triggers when player opens the door.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // Checking if colliding object is the XR Rig
        if (other.CompareTag("MainCamera"))
        {
            Debug.Log(timerLength);
            timerStarted = true;
        }
    }

    public void TimerStop()
    {
        timerStarted = false;
        Debug.Log(timerLength);
        timerLength = 0;
    }
}
