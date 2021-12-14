/*
    The timer is simple, you set a time in the editor, the timer starts when the Restart() function is called which happens in another script. 
    The color of the timer gives an indication on how much time has past. The timer starts green and changes to yellow when only half the time is left. 
    The timer turns red when only 20% of the time is left.
    The if statement containing the transform.eulerAngles.z is to calculate when to show or hide the timer in your right hand. 
    The DisplayTime function formats the timer into minutes and seconds so it shows 01:30 instead of a 90. 
*/
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
        float percentage = (timeRemaining / time) * 100;  // Calculate percentages to change the color of the text.
        if (percentage == 100)
            timeText.color = Color.green;
        else if (percentage < 50 && percentage > 20)
            timeText.color = Color.yellow;
        else if (percentage < 20)
            timeText.color = Color.red;

        if (transform.eulerAngles.z < 300 && transform.eulerAngles.z > 200) // Make sure the timer is only seen when the controller has the correct angle.
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

    void DisplayTime(float timeToDisplay) // Display timer in the correct format.
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    
}
