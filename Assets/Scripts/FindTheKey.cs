using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTheKey : MonoBehaviour
{
    private List<List<int>> distance = new List<List<int>>();
    private List<List<int>> viewingAngle = new List<List<int>>();
    
    [SerializeField] private Timer startTimer;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject keyObject;
    
    private int frameNumber;
    private int lastFrameDistance;    
    private int lastViewAngle;
    [SerializeField] private string pathToDistanceCsv;
    [SerializeField] private string pathToViewingCsv;
    private bool dataSaved;
    private int maximumDistance = 50;

    private void Update()
    {
        if (startTimer.timerStarted)
        {        
            frameNumber++;
            TrackDistance();
            TrackViewingAngle();
        }
        if (startTimer.timerLength == 0 && !dataSaved)
        {
            dataSaved = true;
            SaveDistanceData();
            SaveViewingData();
        }
    }

    private void TrackViewingAngle()
    {
        Vector3 playerToObject = transform.position - player.transform.position;

        if (maximumDistance != 0 && playerToObject.sqrMagnitude > Math.Pow(maximumDistance,2f) && lastViewAngle != -1)
        {
            viewingAngle.Add(new List<int> {frameNumber, -1});
            lastViewAngle = -1;
        }
        else
        {
            playerToObject.Normalize();
            Vector3 lookDirection = player.transform.forward;
            int viewAngle = (int)(100 * Vector3.Dot(playerToObject, lookDirection));
            if (lastFrameDistance != viewAngle)
            {
                Debug.Log(viewAngle); 
                viewingAngle.Add(new List<int> {frameNumber, viewAngle});
            }
            lastViewAngle = viewAngle;
        }
    }

    private void TrackDistance()
    {
        int frameDistance = (int) Vector3.Distance(keyObject.transform.position, player.transform.position);
        if (lastFrameDistance != frameDistance)
        {
            distance.Add(new List<int>{frameNumber, frameDistance});
        }
        lastFrameDistance = frameDistance;
    }
    
    private void SaveViewingData()
    {
        string csv = "";

        foreach (var VARIABLE in viewingAngle)
        {
            csv += $"{VARIABLE[0]},{VARIABLE[1]}\n";
        }

        System.IO.File.WriteAllText(Application.dataPath+pathToViewingCsv, csv);
    }

    private void SaveDistanceData()
    {
        string csv = "";

        foreach (var VARIABLE in distance)
        {
            csv += $"{VARIABLE[0]},{VARIABLE[1]}\n,";
        }

        System.IO.File.WriteAllText(Application.dataPath+pathToDistanceCsv, csv);
    }
}
