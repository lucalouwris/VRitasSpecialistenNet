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
        if (transform.rotation.eulerAngles.y > 60 && !triggered)
        {
            Debug.Log(transform.rotation.eulerAngles.y);
            triggered = true;
            wait();
            meshUpdater.UpdateAllMeshes();
            Debug.Log("UpdateMesh");
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(delay);
    }
}
