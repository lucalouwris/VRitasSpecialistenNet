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
        if (transform.rotation.eulerAngles.x < 69 && !triggered)
        {
            Debug.Log(transform.rotation.eulerAngles.x);
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
