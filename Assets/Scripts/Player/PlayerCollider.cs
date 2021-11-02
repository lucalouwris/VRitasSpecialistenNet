using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private CapsuleCollider collider;
    
    private void FixedUpdate()
    {
        collider.center = new Vector3(0,-transform.position.y / 2,0);
        collider.height = transform.position.y;
    }
}
