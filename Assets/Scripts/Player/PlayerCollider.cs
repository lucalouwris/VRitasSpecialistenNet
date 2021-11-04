using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private CapsuleCollider collider;
    [SerializeField] private GameObject playerHead;
    private void FixedUpdate()
    {
        Vector3 playerOffset = playerHead.transform.position - transform.position ;
        playerOffset.y = playerHead.transform.position.y / 2;
        collider.center = playerOffset;
        collider.height = playerHead.transform.position.y;
    }
}
