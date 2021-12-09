using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseState : BaseState
{
    [SerializeField] Image leftBar;
    [SerializeField] Image rightBar;
    [SerializeField] Text breathStat;

    [SerializeField] string prepTxt, breathInTxt, breathOutTxt, pauseTxt, completedTxt;

    [SerializeField] private float distanceFromPlayer = 3.0f;
    [SerializeField] private GameObject canvasObject;
    [SerializeField] private float timesToRepeat;

    [SerializeField] private float prepTime = 2f;
    [SerializeField] private float boxTime;

    float outTime;
    float inTime;
    float pauseTime;
    float timeRemaining;
    float startTime;
    bool startedExercise = false;
    bool exerciseEnding = false;
    float count;

    private Vector3 goalPos;
    private Vector3 offset;
    [HideInInspector] public enum Stages { preparation, breathIn, breathPause, breathOut, completed };
    Stages currentStage;
    Stages previousStage;

    public override void OnEnable()
    {
        pauseTime = boxTime;
        outTime = boxTime;
        inTime = boxTime;
        base.OnEnable();
        canvasObject.SetActive(true);
        offset = Vector3.down * .3f;
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
        goalPos = Vector3.Scale(goalPos , (Vector3.forward + Vector3.right));
        goalPos += Vector3.up * playerTransform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, goalPos, 2 * Time.deltaTime);
        transform.LookAt(playerTransform);

        if (Vector3.Distance(transform.position, goalPos) < .4f)
        {
            if (!startedExercise)
                SwitchExercise(prepTime, prepTxt, Stages.preparation);
            UpdateExercise();
            if (count >= timesToRepeat && !exerciseEnding)
            {
                exerciseEnding = true;
                SwitchExercise(prepTime, completedTxt, Stages.completed);
            }
        }
    }
   
    void UpdateExercise()
    {
        if (currentStage == Stages.breathIn)
        {
            leftBar.fillAmount = 1.0f - (timeRemaining / startTime);
            rightBar.fillAmount = 1.0f - (timeRemaining / startTime);
        }
        else if (currentStage == Stages.breathPause || currentStage == Stages.preparation || currentStage == Stages.completed)
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
                        count++;
                        break;
                    case Stages.breathPause:
                        if (previousStage == Stages.breathIn) 
                            SwitchExercise(outTime, breathOutTxt, Stages.breathOut);
                        else 
                            SwitchExercise(inTime, breathInTxt, Stages.breathIn);
                        break;
                    case Stages.completed:
                        GetComponent<StateMachine>().SwitchState(GetComponent<StateMachine>().States[1]);
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
