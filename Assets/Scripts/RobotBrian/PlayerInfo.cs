using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public Transform PlayerTransform;

    private void Start()
    {
        if(PlayerTransform = null)
        {
            PlayerTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
    }
}
