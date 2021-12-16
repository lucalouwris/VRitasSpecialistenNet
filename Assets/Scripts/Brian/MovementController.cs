using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MovementController : MonoBehaviour
{
    [SerializeField] Transform PointerTarget;
    [SerializeField] Rig LookAt;
    [SerializeField] Rig PointAt;

    public void ChangePoint(Vector3 newPosition)
    {
        //Change the position of the target to point at
        PointerTarget.position = newPosition;
    }

    public void ResetPoint(Rig rig)
    {
        //Sets the weight of the pointing to 0 the arm just runs the animation
        rig.weight = 0f;
        // WeightedTransformArray weightedTransforms = multiAimConstraintHand.sourceObjects;
        // WeightedTransform weighted = weightedTransforms[0];
        // weighted.weight = 0f;
        // weightedTransforms[0] = weighted;
        // multiAimConstraintHand.sourceObjects = weightedTransforms;
    }

    public void AddPoint(Rig rig)
    {
        //Sets the weight of the pointing to 1
        rig.weight = 1f;
        // WeightedTransformArray weightedTransforms = multiAimConstraintHand.sourceObjects;
        // WeightedTransform weighted = weightedTransforms[0];
        // weighted.weight = 1f;
        // weightedTransforms[0] = weighted;
        // multiAimConstraintHand.sourceObjects = weightedTransforms;
    }

    // public void changeLook(float weight)
    // {
    //     if(weight > 1f)
    //     {
    //         weight = 1f;
    //     } else if (weight<0f)
    //     {
    //         weight = 0f;
    //     }
    //
    //     //Changing the weight of Brian looking at the player
    //     WeightedTransformArray weightedTransformsHead = multiAimConstraintHead.sourceObjects;
    //     WeightedTransform weightedHead = weightedTransformsHead[0];
    //     weightedHead.weight = weight;
    //     weightedTransformsHead[0] = weightedHead;
    //     multiAimConstraintHead.sourceObjects = weightedTransformsHead;
    //
    //     //Change the weight of the weight of the body as a percentage of the weight of the head
    //     float bodyweight = weight * bodyWeightFactor;
    //
    //     WeightedTransformArray weightedTransformsBody = multiAimConstraintBody.sourceObjects;
    //     WeightedTransform weightedBody = weightedTransformsBody[0];
    //     weightedBody.weight = bodyweight;
    //     weightedTransformsBody[0] = weightedBody;
    //     multiAimConstraintBody.sourceObjects = weightedTransformsBody;
    // }

    // Update is called once per frame
    
    [ContextMenu("Change Point")]
    void RandomPoint()
    {
        ChangePoint(Random.onUnitSphere);
    }

    [ContextMenu("Set Pointing")]
    void SetPoint()
    {
        AddPoint(PointAt);
    }

    [ContextMenu("Clear Pointing")]
    void RemovePoint()
    {
        ResetPoint(PointAt);
    }

    [ContextMenu("Set looking")]
    void ModifyPoint()
    {
        AddPoint(LookAt);
    }
    
    [ContextMenu("Clear looking")]
    void RemoveLook()
    {
        ResetPoint(LookAt);
    }
}
