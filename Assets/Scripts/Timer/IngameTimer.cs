using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameTimer : MonoBehaviour
{
    public float time = 100;
    public float timeRemaining;
    public bool timerIsRunning = false;
    [SerializeField] Text timeText;
    [SerializeField] GameObject timerCanvas;
    
    public void Restart()
    {
        timeRemaining = time;
        timerIsRunning = true;
    }

    public void StopTimer()
    {
        timeRemaining = -1;
    }
    void Update()
    {
        float percentage = (timeRemaining / time) * 100;
        if (percentage == 100)
            timeText.color = Color.green;
        else if (percentage < 50 && percentage > 20)
            timeText.color = Color.yellow;
        else if (percentage < 20)
            timeText.color = Color.red;

        if (transform.eulerAngles.z < 300 && transform.eulerAngles.z > 200)
            timerCanvas.SetActive(true);
        else
            timerCanvas.SetActive(false);
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    
}
