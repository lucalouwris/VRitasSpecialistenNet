using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTransition : MonoBehaviour
{
    [SerializeField] private Animator anim;
    bool isFlying = false;
    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && isFlying)
        {  //If normalizedTime is 0 to 1 means animation is playing, if greater than 1 means finished
            CreditsTransition();
        }
    }
    public void Fly()
    {
        anim.SetTrigger("FlyTheShip");
        isFlying = true;
    }
    public void CreditsTransition()
    {

    }
}
