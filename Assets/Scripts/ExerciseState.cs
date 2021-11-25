using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseState : BaseState
{
    [SerializeField] Image leftBar;
    [SerializeField] Image rightBar;
    [SerializeField] Text breathStat;

    [SerializeField] string prepTxt, breathInTxt, breathOutTxt, pauseTxt;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private float distanceFromPlayer = 3.0f;
    [SerializeField] private GameObject canvasObject;

    float prepTime = 5f;
    float outTime = 4f;
    float inTime = 2.5f;
    float pauseTime = 1f;
    float timeRemaining;
    float startTime;
    bool startedExercise = false;

    private Vector3 goalPos;
    private Vector3 offset;
    [HideInInspector] public enum Stages { preparation, breathIn, breathPause, breathOut, completed };
    Stages currentStage;
    Stages previousStage;

    public override void OnEnable()
    {
        base.OnEnable();
        canvasObject.SetActive(true);
        offset = Vector3.right + Vector3.up * .4f;
        currentStage = Stages.preparation;
    }
    public void SwitchExercise(float time, string activeText, Stages nextStage)
    {
        breathStat.text = activeText;
        timeRemaining = time;
        startTime = time;
        previousStage = currentStage;
        currentStage = nextStage;
        startedExercise = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        //Calculating preferred position.
        goalPos = playerTransform.position + playerTransform.forward * distanceFromPlayer + offset;
        transform.position = Vector3.MoveTowards(transform.position, goalPos, .5f * Time.deltaTime);
        transform.LookAt(playerTransform);

        if (Vector3.Distance(transform.position, goalPos) < .4f)
        {
            if (!startedExercise)
                SwitchExercise(prepTime, prepTxt, Stages.preparation);
            UpdateExercise();
        }
    }
   
    void UpdateExercise()
    {
        if (currentStage == Stages.breathIn)
        {
            leftBar.fillAmount = 1.0f - (timeRemaining / startTime);
            rightBar.fillAmount = 1.0f - (timeRemaining / startTime);
        }
        else if (currentStage == Stages.breathPause || currentStage == Stages.preparation)
        {
            leftBar.fillAmount = leftBar.fillAmount;
            rightBar.fillAmount = rightBar.fillAmount;
        }
        else
        {
            leftBar.fillAmount = (timeRemaining / startTime);
            rightBar.fillAmount = (timeRemaining / startTime);
        }

        if (startedExercise)
        {
            if (timeRemaining > 0)
                timeRemaining -= Time.deltaTime;
            else
            {
                switch (currentStage)
                {
                    case Stages.preparation:
                        SwitchExercise(inTime, breathInTxt, Stages.breathIn);
                        break;
                    case Stages.breathIn:
                        SwitchExercise(pauseTime, pauseTxt, Stages.breathPause);
                        break;
                    case Stages.breathOut:
                        SwitchExercise(pauseTime, pauseTxt, Stages.breathPause);
                        break;
                    case Stages.breathPause:
                        if (previousStage == Stages.breathIn) 
                            SwitchExercise(outTime, breathOutTxt, Stages.breathOut);
                        else 
                            SwitchExercise(inTime, breathInTxt, Stages.breathIn);
                        break;
                    case Stages.completed:
                        break;
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnDisable()
    {
        canvasObject.SetActive(false);
    }
}
