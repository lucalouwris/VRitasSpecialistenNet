using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTransition : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] GameObject player;
    [SerializeField] CanvasGroup creditsObject;
    [SerializeField] CanvasGroup TakeOffObject;
    bool isFlying = false;
    public float fadeDuration = 2.5f;

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && isFlying)
        {  //If normalizedTime is 0 to 1 means animation is playing, if greater than 1 means finished
            StartCoroutine(DoFadeInCredits());
            isFlying = false;
        }
    }
    public void Fly()
    {
        StartCoroutine(DoFadeOutTakeOff());
        player.transform.SetParent(this.transform);
        anim.SetTrigger("FlyTheShip");
        isFlying = true;
    }

    public IEnumerator DoFadeInCredits() // Fade the new menu in by activating the menu and increasing the alpha value of the canvas.
    {
        float counter = 0f;
        creditsObject.gameObject.SetActive(true);

        while (counter < fadeDuration)
        {
            counter += Time.deltaTime;
            creditsObject.alpha = Mathf.Lerp(0f, 1f, counter / fadeDuration);
            yield return null;
        }
    }
    public IEnumerator DoFadeOutTakeOff() // Fade the new menu in by activating the menu and increasing the alpha value of the canvas.
    {
        float counter = 0f;

        while (counter < fadeDuration)
        {
            counter += Time.deltaTime;
            TakeOffObject.alpha = Mathf.Lerp(1f, 0f, counter / fadeDuration);
            yield return null;
        }
        TakeOffObject.gameObject.SetActive(false);
    }
}
