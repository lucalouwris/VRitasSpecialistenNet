using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerDoor : MonoBehaviour
{
    
    [SerializeField] private Animator ObjectAnimator;
    [SerializeField] private string animatorTrigger;
    [SerializeField] private AudioSource doorSoundSource;
    [SerializeField] private AudioClip openDoorSound;
    AudioController audioController;


    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ObjectAnimator.SetTrigger(animatorTrigger);
            doorSoundSource.PlayOneShot(openDoorSound);
            this.gameObject.SetActive(false);
            audioController.PlayPressure();
        }
    }
}
