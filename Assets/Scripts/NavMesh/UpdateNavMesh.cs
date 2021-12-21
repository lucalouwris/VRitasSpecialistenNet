using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class UpdateNavMesh : MonoBehaviour
{
    [SerializeField] private NavMeshSurface[] surfaces;

    [ContextMenu("Update nav Meshes")]
    public void UpdateAllMeshes()
    {
        foreach (var surface in surfaces)
        {
            surface.BuildNavMesh();
        }
    }
}
