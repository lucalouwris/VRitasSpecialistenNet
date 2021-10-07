using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;

public class EmergencyExit : MonoBehaviour
{
    // a reference to the action
    public SteamVR_Action_Boolean exitGrip;
    // a reference to the hands
    public SteamVR_Input_Sources leftHand;
    public SteamVR_Input_Sources rightHand;

    [SerializeField] Image loadingBar;

    string sceneName;
    bool leftActive = false;
    bool rightActive = false;
    float timer = 2.0f;
    float currentTimer;
    // Start is called before the first frame update
    void Start()
    {
        loadingBar.enabled = false;
        currentTimer = timer;
        exitGrip.AddOnStateDownListener(TriggerDownLeft, leftHand);
        exitGrip.AddOnStateUpListener(TriggerUpLeft, leftHand);
        exitGrip.AddOnStateDownListener(TriggerDownRight, rightHand);
        exitGrip.AddOnStateUpListener(TriggerUpRight, rightHand);
    }
    public void TriggerUpLeft(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        leftActive = false;
    }
    public void TriggerDownLeft(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        leftActive = true;
    }
    public void TriggerUpRight(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        rightActive = false;
    }
    public void TriggerDownRight(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        rightActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Testing only
        if (Input.GetKey("t"))
        {
            leftActive = true;
            rightActive = true;
        }
        else
        {
            leftActive = false;
            rightActive = false;
        }
        ///////////////////////

        loadingBar.fillAmount = (currentTimer / timer);
        if (rightActive && leftActive)
        {
            loadingBar.enabled = true;
            if (currentTimer > 0)
                currentTimer -= Time.deltaTime;
            else
                SceneManager.LoadScene("UIScene");
        }
        else
        {
            currentTimer = timer;
            loadingBar.enabled = false;
        }
    }
}
