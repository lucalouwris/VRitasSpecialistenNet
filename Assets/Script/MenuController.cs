using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class MenuController : MonoBehaviour
{
    public Menus[] Menu;
    [Serializable]
    public struct Menus
    {
        public GameObject Menu;
        public bool faded;
    }
    [SerializeField] string[] environments;
    [SerializeField] GameObject envPanel;
    [SerializeField] GameObject envButton;

    public float fadeDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Menu.Length; i++)
        {
            var canvGroup = Menu[i].Menu.GetComponent<CanvasGroup>();
            bool fade = Menu[i].faded;
            canvGroup.alpha = fade ? 0 : 1;
            Menu[i].Menu.SetActive(!fade);
        }
        SpawnEnvButtons();
    }
    public void StartProgram()
    {
        Fade(Menu[0], Menu[1]);
    }
    public void ExitProgram()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    } 

    public void Fade(Menus menuClose, Menus menuOpen)
    {
        var canvGroupClose = menuClose.Menu.GetComponent<CanvasGroup>();
        StartCoroutine(DoFadeOut(canvGroupClose, canvGroupClose.alpha, menuClose.faded ? 1 : 0, menuClose, menuOpen));
    }

    public IEnumerator DoFadeOut(CanvasGroup canvGroup, float start, float end, Menus menuClose, Menus menuOpen)
    {
        float counter = 0f;
        while (counter < fadeDuration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / fadeDuration);
            
            yield return null;
        }
        menuClose.Menu.SetActive(menuClose.faded);
        var canvGroupOpen = menuOpen.Menu.GetComponent<CanvasGroup>();
        StartCoroutine(DoFadeIn(canvGroupOpen, canvGroupOpen.alpha, menuOpen.faded ? 1 : 0, menuOpen));
    }
    public IEnumerator DoFadeIn(CanvasGroup canvGroup, float start, float end, Menus menuOpen)
    {
        float counter = 0f;
        menuOpen.Menu.SetActive(menuOpen.faded);

        while (counter < fadeDuration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / fadeDuration);

            yield return null;
        }
    }
    public void SpawnEnvButtons()
    {
        float xStart = -0.4f;
        float yStart = 1.32f;
        float xSpacing = 0.8f;
        float ySpacing = 0.64f;
        int colLength = 2;

        for (int i = 0; i < environments.Length; i++)
        {
            GameObject button = Instantiate(envButton, new Vector3(xStart + (xSpacing * (i % colLength)), yStart + (-ySpacing * (i / colLength)), -0.1f), Quaternion.identity, envPanel.transform);
            ButtonListener but = button.GetComponentInChildren<ButtonListener>();
            but.envNumber = i;
            but.envName = environments[i];
        }
    }
    public void SwitchScene(int sceneNumber)
    {
        SceneManager.LoadScene(environments[sceneNumber]);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
