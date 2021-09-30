using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    AudioSource crowdedRoom;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        crowdedRoom = GetComponent<AudioSource>();

        crowdedRoom.Play();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        crowdedRoom.volume = timer / 200;
    }
}
