using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTrack : FindTheKey
{    
    private int frameNumber;
    public Dictionary<int, int> Distance = new Dictionary<int, int>();
    private int lastFrameDistance;
    private void Update()
    {
        if(startTimer.timerStarted)
        {
            frameNumber++;
            int frameDistance = (int) Vector3.Distance(keyObject.transform.position, player.transform.position);
            if (lastFrameDistance != frameDistance)
            {
                Debug.Log(frameDistance);
                Distance.Add(frameNumber, frameDistance);
            }
            lastFrameDistance = frameDistance;
        }    
    }
}
