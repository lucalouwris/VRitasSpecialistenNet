using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MeshAfterAnimation : MonoBehaviour
{
    [SerializeField] private float delay = .5f;
    [SerializeField] private UpdateNavMesh meshUpdater;

    private bool firstTriggered = false;
    private bool secondTriggered = false;
    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y < -4.8f && !firstTriggered)
        {
            Debug.Log(transform.localPosition.y);
            StartCoroutine("wait");
            meshUpdater.UpdateAllMeshes();
            secondTriggered = true;
            
        }
        if (transform.localPosition.y > -2.35f && secondTriggered)
        {
            secondTriggered = false;
            StartCoroutine("wait");
            meshUpdater.UpdateAllMeshes();
        }
    }

    IEnumerator wait()
    {
        firstTriggered = true;
        yield return new WaitForSeconds(delay);
        Debug.Log("UpdateMesh");

        yield return null;
    }
}
