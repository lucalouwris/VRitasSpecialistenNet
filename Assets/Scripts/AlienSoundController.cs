using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSoundController : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip alienSound;
    private float intervalCount;
    private float randomInterval = 10;
    private float startInterval = 10f;
    private float endInterval = 20f;

    // Update is called once per frame
    void Update()
    {        
        intervalCount += Time.deltaTime;
        if(randomInterval < intervalCount)
        {
            randomInterval = Random.Range(startInterval, endInterval);
            audioSource.PlayOneShot(alienSound);
            intervalCount = 0;
        }
    }
}
