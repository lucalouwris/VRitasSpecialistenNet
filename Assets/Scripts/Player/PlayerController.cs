using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CapsuleCollider cCollider;
    [SerializeField] private Rigidbody playerBody;
    [SerializeField] private GameObject playerHead;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private BrianSays brianSays;

    public BrianSays BrianSays => brianSays;

    private XRRig rig;

    private void Start()
    {
        rig = GetComponent<XRRig>();
    }

    private void FixedUpdate()
    {
        // Match collider with headset pos.
        FollowHeadset();

        // If grounded then don't use gravity on player.
        bool isGrounded = Grounded();
        playerBody.useGravity = !isGrounded;
    }

    /// <summary>
    /// Adding the 6DOF by changing the position of the collider based on where the player is in real space.
    /// </summary>
    private void FollowHeadset()
    {
        cCollider.height = rig.cameraInRigSpaceHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        cCollider.center = new Vector3(capsuleCenter.x, cCollider.height / 2, capsuleCenter.z);
    }

    /// <summary>
    /// Checking if the user is on the ground.
    /// </summary>
    /// <returns></returns>
    private bool Grounded()
    {
        Vector3 checkGroundedPos = playerHead.transform.position + Vector3.up * .1f;
        return Physics.SphereCast(checkGroundedPos, 0.2f, Vector3.down, out RaycastHit hit, cCollider.height + 0.01f, groundMask);
    }
}