using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DistanceTrack : FindTheKey
{    
    private int frameNumber;
    public List<List<int>> Distance = new List<List<int>>();
    private int lastFrameDistance;
    [SerializeField] private string pathToCsv;
    private bool dataSaved = false;
    private void Update()
    {
        if(startTimer.timerStarted)
        {
            frameNumber++;
            int frameDistance = (int) Vector3.Distance(keyObject.transform.position, player.transform.position);
            if (lastFrameDistance != frameDistance)
            {
                Debug.Log(frameDistance);
                Distance.Add(new List<int>{frameNumber, frameDistance});
            }
            lastFrameDistance = frameDistance;
        }
        
        if (startTimer.timerLength == 0 && !dataSaved)
        {
            dataSaved = true;
            
            string csv = "";

            foreach (var VARIABLE in Distance)
            {
                csv += $"{VARIABLE[0]},{VARIABLE[1]},";
            }
            Debug.Log(csv);

            System.IO.File.WriteAllText(Application.dataPath+pathToCsv, csv);
        }
    }
}
