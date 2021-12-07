using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioController : MonoBehaviour
{
[SerializeField] private AudioSource backgroundSource;
[SerializeField] private AudioClip backgroundMusic;
[SerializeField] private AudioClip backgroundPressureMusic;

    // Start is called before the first frame update
    void Start()
    {
        backgroundSource.clip = backgroundMusic;
        backgroundSource.Play();
    }
    // Play background pressure music
    public void PlayPressure()
    {
        backgroundSource.Stop();
        backgroundSource.clip = backgroundPressureMusic;
        backgroundSource.Play();
    }
    // Play background normal music
    public void PlayBackground()
    {
        backgroundSource.Stop();
        backgroundSource.clip = backgroundMusic;
        backgroundSource.Play();
    }
}