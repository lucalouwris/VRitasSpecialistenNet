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
    float zRotation = 220;
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

        if (Watch != null)
        {
            if (transform.localEulerAngles.y > yRotation && transform.localEulerAngles.z < zRotation && transform.localScale != watchFocusScale)
            {
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
        while (!hasLookedAtWatch)
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

        switch (pressType)
        {
            case PressType.Continuous: active = isPressed; break;
            case PressType.Begin: active = isPressed && !wasPressed; break;
            case PressType.End: active = !isPressed && wasPressed; break;
        }

        if (active) OnActive.Invoke();
        wasPressed = isPressed;

    }
}

public enum XRButton
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

public enum PressType
{
    Begin,
    End,
    Continuous
}

public static class XRStatics
{
    public static InputFeatureUsage<bool> GetFeature(XRButton button)
    {
        switch (button)
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