using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTransition : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Animator transition;
    [SerializeField] GameObject player;
    bool isFlying = false;
    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && isFlying)
        {  //If normalizedTime is 0 to 1 means animation is playing, if greater than 1 means finished
            CreditsTransition();
            isFlying = false;
        }
    }
    public void Fly()
    {
        player.transform.SetParent(this.transform);
        anim.SetTrigger("FlyTheShip");
        isFlying = true;
    }
    public void CreditsTransition()
    {
        transition.SetTrigger("Fade");
    }
}
