using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTransition : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] GameObject player;
    [SerializeField] CanvasGroup creditsObject;
    [SerializeField] CanvasGroup TakeOffObject;
    public float fadeDuration = 2.5f;

    public void Fly()
    {
        TakeOffObject.gameObject.SetActive(false);
        StartCoroutine(DoFadeInCredits());
        player.transform.SetParent(this.transform);
        anim.SetTrigger("FlyTheShip");
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
}
