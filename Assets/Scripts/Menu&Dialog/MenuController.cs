using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Menus[] Menu;
    [SerializeField] GameObject prev, next;

    [Serializable]
    public class Menus
    {
        public GameObject Menu;
        public bool active;
    }

    public float fadeDuration = 0.5f;
    int numMenu = 0;

    // Start is called before the first frame update
    void Start()
    {
        CheckMenus();
        if (prev != null && numMenu == 0)
            prev.SetActive(false);
    }
    void CheckMenus()
    {
        for (int i = 0; i < Menu.Length; i++)
        {
            var canvGroup = Menu[i].Menu.GetComponent<CanvasGroup>();
            bool active = Menu[i].active;
            canvGroup.alpha = active ? 0 : 1;
            Menu[i].Menu.SetActive(!active);
        }
    }
    public void StartGame()
    {
        
    }
    public void Next()
    {
        if (numMenu <= Menu.Length)
        {
            Fade(Menu[numMenu], Menu[numMenu + 1]);
            numMenu++;
            prev.SetActive(true);
            if (numMenu == Menu.Length-1)
                next.SetActive(false);
        }
    }
    public void Previous()
    {
        if(numMenu > 0)
        {
            numMenu--;
            Fade(Menu[numMenu + 1], Menu[numMenu]);
            next.SetActive(true);
        }
        if(numMenu == 0)
            prev.SetActive(false);
    }
    public void ExitGame()
    {
        //if (Application.isEditor)
          //  UnityEditor.EditorApplication.isPlaying = false;
        //else
            Application.Quit();
    } 

    public void Fade(Menus menuClose, Menus menuOpen)
    {
        var canvGroupClose = menuClose.Menu.GetComponent<CanvasGroup>();
        StartCoroutine(DoFadeOut(canvGroupClose, canvGroupClose.alpha, menuClose.active ? 1 : 0, menuClose, menuOpen));
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
        menuClose.Menu.SetActive(menuClose.active);
        menuClose.active = true;
        var canvGroupOpen = menuOpen.Menu.GetComponent<CanvasGroup>();
        StartCoroutine(DoFadeIn(canvGroupOpen, canvGroupOpen.alpha, menuOpen.active ? 1 : 0, menuOpen));
    }
    public IEnumerator DoFadeIn(CanvasGroup canvGroup, float start, float end, Menus menuOpen)
    {
        float counter = 0f;
        menuOpen.Menu.SetActive(menuOpen.active);

        while (counter < fadeDuration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / fadeDuration);

            yield return null;
        }
        menuOpen.active = false;
    }
}
