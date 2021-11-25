using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    public Vector3 PlayerPos;

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPos = playerTransform.position;
    }
}
