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
    [SerializeField] float wait = 0.7f;

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

        if (!(currRotation <= 325 && currRotation > 180))
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
            if (empty)
            {
                controller.SendHapticImpulse(feedbackStrength, feedbackLength);
               if(ReadHeartrate.currPulse != 0)
                {
                    Debug.Log("Current heartrate is: " + ReadHeartrate.currPulse);
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
    public void fillOxygen()
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
