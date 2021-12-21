/*
    This script shows an arrow object in front of the player when Brian wants to talk to you.
    The arrow points in the direction of Brian so you know where he is when he is not in your viewport.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class BrianPointer : MonoBehaviour
{
    public GameObject pointer;
    public Material mat;
    public GameObject target;
    public Vector3 offset;
    [HideInInspector] public bool isActive;

    private void Start()
    {
        isActive = false;
    }

    private void Update()
    {
        if (isActive)
        {
            if (!pointer.activeSelf)
                pointer.SetActive(true);

            Vector3 relativePos = target.transform.position - transform.position;
            Quaternion LookAtRotation = Quaternion.LookRotation(relativePos);
            Quaternion LookAtRotationXY = Quaternion.Euler(0f, LookAtRotation.eulerAngles.y, 0f);
            pointer.transform.rotation = LookAtRotationXY;
        }
        else
            pointer.SetActive(false);
    }
}
