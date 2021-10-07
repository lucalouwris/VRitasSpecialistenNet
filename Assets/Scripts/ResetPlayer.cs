using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        if (player != null)
            player.transform.position = transform.position;
    }
}
