using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioController : MonoBehaviour
{
[SerializeField] private static AudioSource backgroundSource;
[SerializeField] private static AudioClip backgroundMusic;
[SerializeField] private static AudioClip backgroundPressureMusic;



    // Start is called before the first frame update
    void Start()
    {
        backgroundSource.PlayOneShot(backgroundMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static void playPressure()
    {
        backgroundSource.PlayOneShot(backgroundPressureMusic);
    }
}
