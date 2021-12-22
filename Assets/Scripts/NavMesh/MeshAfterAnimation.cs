using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MeshAfterAnimation : MonoBehaviour
{
    [SerializeField] private float delay = .5f;
    [SerializeField] private UpdateNavMesh meshUpdater;

    private bool triggered = false;
    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y < -4.8f && !triggered)
        {
            Debug.Log(transform.localPosition.y);
            StartCoroutine("wait");
            meshUpdater.UpdateAllMeshes();
        }
    }

    IEnumerator wait()
    {
        triggered = true;
        yield return new WaitForSeconds(delay);
        Debug.Log("UpdateMesh");

        yield return null;
    }
}
