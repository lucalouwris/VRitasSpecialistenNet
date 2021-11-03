using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DistanceTrack : FindTheKey
{    
    private int frameNumber;
    public List<List<int>> Distance = new List<List<int>>();
    public List<List<int>> ViewingAngle = new List<List<int>>();
    private int lastFrameDistance;    
    private int lastViewAngle;
    [SerializeField] private string pathToDistanceCsv;
    [SerializeField] private string pathToViewingCsv;
    private bool dataSaved;
    private int maximumDistance = 5;

    private void Update()
    {
        if (startTimer.timerStarted)
        {
            TrackDistance();
            TrackViewingAngle();
        }
        if (startTimer.timerLength == 0 && !dataSaved)
        {
            dataSaved = true;
            SaveDistanceData();
            SaveViewingData();
        }
        frameNumber++;
    }

    private void SaveViewingData()
    {
        string csv = "";

        foreach (var VARIABLE in ViewingAngle)
        {
            csv += $"{VARIABLE[0]},{VARIABLE[1]},";
        }

        System.IO.File.WriteAllText(Application.dataPath+pathToViewingCsv, csv);
    }

    private void SaveDistanceData()
    {
        string csv = "";

        foreach (var VARIABLE in Distance)
        {
            csv += $"{VARIABLE[0]},{VARIABLE[1]},";
        }

        System.IO.File.WriteAllText(Application.dataPath+pathToDistanceCsv, csv);
    }

    private void TrackViewingAngle()
    {
        Vector3 playerToObject = transform.position - player.transform.position;

        if (maximumDistance != 0 && playerToObject.sqrMagnitude > Math.Pow(maximumDistance,2f))
        {
            ViewingAngle.Add(new List<int> {frameNumber, -1});
        }
        else
        {
            playerToObject.Normalize();
            Vector3 lookDirection = player.transform.forward;
            int viewAngle = (int) Vector3.Dot(playerToObject, lookDirection);
            if (lastFrameDistance != viewAngle)
            {
                ViewingAngle.Add(new List<int> {frameNumber, viewAngle});
            }
            lastViewAngle = viewAngle;
        }
    }

    private void TrackDistance()
    {
        int frameDistance = (int) Vector3.Distance(keyObject.transform.position, player.transform.position);
        if (lastFrameDistance != frameDistance)
        {
            Distance.Add(new List<int>{frameNumber, frameDistance});
        }
        lastFrameDistance = frameDistance;
    }
}
