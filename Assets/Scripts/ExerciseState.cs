using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseState : MonoBehaviour
{
    [SerializeField] Image leftBar;
    [SerializeField] Image rightBar;
    [SerializeField] Text breathStat;
    [SerializeField] Text breathStatNumber;

    float time = 100;
    float timeRemaining;
    bool timerIsRunning = false;
    // Start is called before the first frame update
    public void UpdateExercise()
    {
        timeRemaining = time;
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        leftBar.fillAmount -= 1.0f / timeRemaining;
        rightBar.fillAmount -= 1.0f / timeRemaining;
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {

            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        breathStatNumber.text = string.Format("{0:0}", seconds);
    }
}
