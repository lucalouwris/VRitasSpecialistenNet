using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshAfterAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float clip = 3.33f;
    [SerializeField] private UpdateNavMesh meshUpdater;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("DoorOpens"))
        {
            wait();
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(clip);
        meshUpdater.UpdateAllMeshes();
    }
}
