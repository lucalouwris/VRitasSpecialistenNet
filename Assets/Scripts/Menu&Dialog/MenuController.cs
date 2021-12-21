/*
    MenuController creates a smooth fading transition between two menus. 
    In the first sprint it is used to transition between the main menu which contained a start and exit button and the environment menu which allows the player to transition to an environment that contains a specific activity. 
    The transition happens with two Coroutines that first fades out the CanvasGroup by reducing the alpha of the element and then deactivates it. The second Coroutine enables the other CanvasGroup and then increases the alpha.
*/
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
        for (int i = 0; i < Menu.Length; i++) // Se the correct menu items hidden or visible
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
    public void Next() // The menu it is transitioning to.
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
    public void Previous() // The previous menu it is transitioning from.
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
    public void ExitGame() // Exit the Game.
    {
            Application.Quit();
    }

    public void Fade(Menus menuClose, Menus menuOpen) // Start the fade transition
    {
        var canvGroupClose = menuClose.Menu.GetComponent<CanvasGroup>();
        StartCoroutine(DoFadeOut(canvGroupClose, canvGroupClose.alpha, menuClose.active ? 1 : 0, menuClose, menuOpen));
    }

    public IEnumerator DoFadeOut(CanvasGroup canvGroup, float start, float end, Menus menuClose, Menus menuOpen) // Fade the previous menu out by reducing the alpha value of the canvas and then deactivating it.
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
    public IEnumerator DoFadeIn(CanvasGroup canvGroup, float start, float end, Menus menuOpen) // Fade the new menu in by activating the menu and increasing the alpha value of the canvas.
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
