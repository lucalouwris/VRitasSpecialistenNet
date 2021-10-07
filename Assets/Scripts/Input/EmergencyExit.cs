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

    Scene scene;
    string sceneName;

    bool leftActive = false;
    bool rightActive = false;
    float timer = 2.0f;
    float currentTimer;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        loadingBar.enabled = false;
        currentTimer = timer;
    }
    
    // Update is called once per frame
    void Update()
    {
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        if (exitGrip.GetState(SteamVR_Input_Sources.LeftHand))
            leftActive = true;
        else
            leftActive = false;

        if (exitGrip.GetState(SteamVR_Input_Sources.RightHand))
            rightActive = true;
        else
            rightActive = false;

        loadingBar.fillAmount = (currentTimer / timer);
        if (rightActive && leftActive && sceneName != "UIScene")
        {
            loadingBar.enabled = true;
            if (currentTimer > 0)
                currentTimer -= Time.deltaTime;
            else
            {
                SceneManager.LoadScene("UIScene");
                currentTimer = timer;
            }
        }
        else
        {
            currentTimer = timer;
            loadingBar.enabled = false;
        }
    }
}
