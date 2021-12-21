using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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

    [SerializeField] private DialogueObject[] dialogueObjects;
    [SerializeField] private BrianSays speaker;

    private int dialogueCount = 0;


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
        count = 0;

        exerciseEnding = false;
        startedExercise = false;

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
                        this.speaker.playThis = dialogueObjects[dialogueCount];
                        dialogueCount++;
                        break;
                }
            }
        }
    }
    
    private Vector3 GetRandomPosition()
    {
        RaycastHit ForwardHit;
        //Vector2 pos = Random.onUnitSphere * 2.5f;
        Vector3 calculatedPos = playerTransform.position; //new Vector3(pos.x, .5f, pos.y) + 
        Vector3 direction = new Vector3(playerTransform.forward.x, 0, playerTransform.forward.z).normalized;

        calculatedPos += direction;
        Vector3 wantedPos = calculatedPos;
        
        if(Physics.Raycast(calculatedPos, direction, out ForwardHit, distanceFromPlayer))
        {
            wantedPos += direction * (ForwardHit.distance * .75f);
            wantedPos = CheckDown(wantedPos);
        }
        else
        {
            wantedPos += direction * distanceFromPlayer;
            wantedPos = CheckDown(wantedPos);
        }

        return wantedPos;
    }

    private Vector3 CheckDown(Vector3 checkPos)
    {
        RaycastHit downHit;
        checkPos.y -= 0.1f;
        if (Physics.Raycast(checkPos, Vector3.down, out downHit, 10f))
        {
            return SampleHit(downHit.point);
        }
        return SampleHit(checkPos);
    }
    private Vector3 SampleHit(Vector3 checkPos)
    {
        NavMeshHit myNavHit;
        if (NavMesh.SamplePosition(checkPos, out myNavHit, 100f, NavMesh.AllAreas))
        {
            return myNavHit.position;
        }
        return checkPos;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnDisable()
    {
        breathStat.text = "";
        canvasObject.SetActive(false);
    }
}
