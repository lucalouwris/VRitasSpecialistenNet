using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class UpdateNavMesh : MonoBehaviour
{
    [SerializeField] NavMeshAgent brian;
    [SerializeField] GameObject warp;
    [SerializeField] private Unity.AI.Navigation.NavMeshSurface[] surfaces;

    [ContextMenu("Update nav Meshes")]
    public void UpdateAllMeshes()
    {
        brian.Warp(warp.transform.position);
        brian.SetDestination(warp.transform.position);
    }
}
