/*
    The script makes it possible to assign buttons on the controller to run specific functions. 
    The list of buttons are inside the public enum XRButton. 
    The type of button press can be chosen with the public enum PressType:
    - Begin: Single Press
    - End: When you stop pressing
    - Continuous: Holding the button
 */

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerInput : MonoBehaviour
{
    [SerializeField] XRController controller;
    [SerializeField] XRBinding[] bindings;
    [HideInInspector] public Vector3 previousPosition, currentPosition;
    [HideInInspector] public float c_Movement;
    [HideInInspector] public bool isWalking;
    [SerializeField] XRInteractorLineVisual line;

    [SerializeField] GameObject Watch;
    Vector3 watchOriginalScale = new Vector3(0.01f, 0.01f, 0.01f);
    Vector3 watchFocusScale = new Vector3(0.02f, 0.02f, 0.02f);
    float yRotation = 70;
    float zRotationMax = 230;
    float zRotationMin = 140;
    float feedbackStrength = 0.8f;
    float feedbackLength = 0.75f;
    bool hasLookedAtWatch = false;

    private void Start()
    {
        Feedback();
    }

    private void Update()
    {
        isWalking = false;
        c_Movement = 0f;

        foreach (var binding in bindings)
            binding.Update(controller.inputDevice);

        if (Watch != null) // If watch is on controller. In this case the left.
        {
            if (transform.eulerAngles.y > yRotation && transform.eulerAngles.z < zRotationMax && transform.eulerAngles.z > zRotationMin && transform.localScale != watchFocusScale)
            {
                // If the rotation of the controller matches the criteria.
                hasLookedAtWatch = true;
                Watch.transform.localScale = watchFocusScale;
            }
            else
                Watch.transform.localScale = watchOriginalScale;
        }
    }
    public void Walk()
    {
        isWalking = true;
    }
    public void Feedback()
    {
        if (!hasLookedAtWatch && Watch != null)
            StartCoroutine(Haptic());
    }
    IEnumerator Haptic()
    {
        while (!hasLookedAtWatch) // While the player has not looked at the watch yet. Vibrate the controller.
        {
            controller.SendHapticImpulse(feedbackStrength, feedbackLength);
            yield return new WaitForSeconds(feedbackLength + 2f);
        }
        yield return null;
    }
}

[Serializable]
public class XRBinding
{
    [SerializeField] XRButton button;
    [SerializeField] PressType pressType;
    [SerializeField] UnityEvent OnActive;

    bool isPressed;
    bool wasPressed;

    public void Update(InputDevice device)
    {
        device.TryGetFeatureValue(XRStatics.GetFeature(button), out isPressed); 
        bool active = false;

        switch (pressType) // Check what binding type we defined in the inspector.
        {
            case PressType.Continuous: active = isPressed; break;
            case PressType.Begin: active = isPressed && !wasPressed; break;
            case PressType.End: active = !isPressed && wasPressed; break;
        }

        if (active) OnActive.Invoke(); // Check if button is active. If yes, invoke the function defined in the inspector
        wasPressed = isPressed;

    }
}

public enum XRButton // List of buttons on the controller.
{
    Trigger,
    Grip,
    Primary,
    PrimaryTouch,
    Secondary,
    SecondaryTouch,
    Primary2DAxisClick,
    Primary2DAxisTouch
}

public enum PressType // The type of press you want to use. Begin = single press, end = single release, continuous = while pressing.
{
    Begin,
    End,
    Continuous
}

public static class XRStatics
{
    public static InputFeatureUsage<bool> GetFeature(XRButton button)
    {
        switch (button) // List of buttons on the controller.
        {
            case XRButton.Trigger: return CommonUsages.triggerButton;
            case XRButton.Grip: return CommonUsages.gripButton;
            case XRButton.Primary: return CommonUsages.primaryButton;
            case XRButton.PrimaryTouch: return CommonUsages.primaryTouch;
            case XRButton.Secondary: return CommonUsages.secondaryButton;
            case XRButton.SecondaryTouch: return CommonUsages.secondaryTouch;
            case XRButton.Primary2DAxisClick: return CommonUsages.primary2DAxisClick;
            case XRButton.Primary2DAxisTouch: return CommonUsages.primary2DAxisTouch;
            default: Debug.LogError("button " + button + " not found"); return CommonUsages.triggerButton;
        }
    }
}