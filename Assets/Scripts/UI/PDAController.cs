using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PDAController : MonoBehaviour
{
    [SerializeField] private Transform clockHandTransform;
    [SerializeField] XRController controller;
    [SerializeField] public float rotationSpeed = 800; //Number of seconds it takes to complete 90 degrees
    [SerializeField] float feedbackStrength = 0.5f;
    [SerializeField] float feedbackLength = 0.25f;
    [SerializeField] float wait = 0.7f;                 //Wait between pulsations

    private float currRotation;
    private float rotation = 90;     //90 degrees a second
    private bool empty = false;

    private void Start()
    {
        StartCoroutine(Haptic());
    }
    private void Update()
    {
        currRotation = clockHandTransform.localEulerAngles.y;

        if (!(currRotation <= 325 && currRotation > 180)) //If the indicator is not empty
        {
            clockHandTransform.localEulerAngles -= new Vector3(0, Time.deltaTime * rotation / rotationSpeed, 0);
        } else
        {
            empty = true;
        }
    }

    IEnumerator Haptic()
    {
        while (true)
        {
            if (empty) //If the indicator is empty, start pulsation on controller to increase stress
            {
                controller.SendHapticImpulse(feedbackStrength, feedbackLength);
               if(ReadHeartrate.currPulse > 0) //Heartrate is set to 0 if it is not received in the other, this also ensures other errors like Null
                {
                    yield return new WaitForSeconds(feedbackLength + (60/ReadHeartrate.currPulse));
                } else
                {
                    yield return new WaitForSeconds(feedbackLength + wait);
                }
            } else
            {
                yield return null;
            }
        }
    }
    public void fillOxygen() //Set the indicator to the top again
    {
        clockHandTransform.localEulerAngles = new Vector3(0, 45, 0);
        empty = false;
    }

    [ContextMenu("Fill oxygen")]
    void ResetFromInspector()
    {
        fillOxygen();
    }
}
