using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioController : MonoBehaviour
{
[SerializeField] private AudioSource backgroundSource;
[SerializeField] private AudioClip backgroundMusic;
[SerializeField] private AudioClip backgroundPressureMusic;
[SerializeField] private AudioClip backgroundAlienMusic;

    // Start is called before the first frame update
    void Start()
    {
        backgroundSource.clip = backgroundMusic;
        backgroundSource.Play();
    }
    public void PlayPressure()
    {
        backgroundSource.Stop();
        backgroundSource.clip = backgroundPressureMusic;
        backgroundSource.Play();
    }
    public void PlayBackground()
    {
        backgroundSource.Stop();
        backgroundSource.clip = backgroundMusic;
        backgroundSource.Play();
    }

    public void PlayAlien()
    {
        backgroundSource.Stop();
        backgroundSource.clip = backgroundAlienMusic;
        backgroundSource.Play();
    }
}
