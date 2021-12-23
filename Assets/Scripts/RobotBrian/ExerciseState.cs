using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;

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

    [SerializeField] private DialogueObject[] dialogueObjects;
    [SerializeField] private BrianSays speaker;
    public static Action<string> state;

    private int dialogueCount = 0;

    [SerializeField] private float inTime;
    [SerializeField] private float outTime;
    [SerializeField] private float pauseTime;

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
        base.OnEnable();
        count = 0;

        exerciseEnding = false;
        startedExercise = false;
        
        goalPos = GetRandomPosition();
        navAgent.SetDestination(goalPos);
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
        // Calculating goalPos
        if (Vector3.Distance(playerTransform.position, goalPos) > distanceFromPlayer)
        {
            goalPos = GetRandomPosition();
            navAgent.SetDestination(goalPos);
        }

        if (Vector3.Distance(transform.position, goalPos) < 5f)
        {
            transform.LookAt(playerTransform);
            if (!startedExercise)
            {
                startedExercise = true;
                canvasObject.SetActive(true);
                offset = Vector3.down * .3f;
                currentStage = Stages.preparation;
                state.Invoke("Breathing");
                SwitchExercise(prepTime, prepTxt, Stages.preparation);
            }
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
                        SwitchExercise(inTime, breathInTxt, Stages.breathIn);
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
                        speaker.playThis = dialogueObjects[dialogueCount];
                        dialogueCount++;
                        break;
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnDisable()
    {
        breathStat.text = "";
        canvasObject.SetActive(false);
    }
}
