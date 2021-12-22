using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CapsuleCollider cCollider;
    [SerializeField] private Rigidbody playerBody;
    [SerializeField] private GameObject playerHead;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private BrianSays brianSays;
    [SerializeField] private NavMeshObstacle meshObstacle;

    [SerializeField] ControllerInput leftController;
    [SerializeField] ControllerInput rightController;
    [SerializeField] float speedModifier = 2;

    RaycastHit slopeHit;

    public BrianSays BrianSays => brianSays;

    [HideInInspector] public Vector3 slopeMoveDirection;

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
         playerBody.useGravity = !isGrounded();

        // Calculate distance travelled of left controller.
        leftController.previousPosition = leftController.transform.position;
        leftController.c_Movement = (Vector3.Distance(leftController.currentPosition, leftController.previousPosition) / Time.deltaTime);
        // Calculate distance travelled of right controller.
        rightController.previousPosition = rightController.transform.position;
        rightController.c_Movement = (Vector3.Distance(rightController.currentPosition, rightController.previousPosition) / Time.deltaTime);

        if (leftController.isWalking || rightController.isWalking)
        {
            // If either of the controllers has their grip button pressed. Use the distance travelled to move.
            Vector3 direction = Camera.main.transform.forward;
            slopeMoveDirection = Vector3.ProjectOnPlane(direction, slopeHit.normal);
            //direction.y = 0;

            float moveDistance = leftController.c_Movement + rightController.c_Movement;

            if(isGrounded() && !OnSlope())
                playerBody.AddForce((direction * moveDistance) * speedModifier);
            else if(isGrounded() && OnSlope())
                playerBody.AddForce((slopeMoveDirection * moveDistance) * speedModifier);
            else
                playerBody.AddForce((direction * moveDistance) * speedModifier);
        }
        else
        {
            // Else if no grips are pressed.
            playerBody.velocity = new Vector3(0, playerBody.velocity.y, 0);
        }
        leftController.currentPosition = leftController.transform.position;
        rightController.currentPosition = rightController.transform.position;
    }
    private bool isGrounded()
    {
        Vector3 checkGroundedPos = playerHead.transform.position + Vector3.up * .1f;
        return Physics.CheckSphere(checkGroundedPos, cCollider.height + 0.01f, groundMask);
    }

    private bool OnSlope()
    {
        Vector3 checkGroundedPos = playerHead.transform.position + Vector3.up * .1f;
        if (Physics.Raycast(checkGroundedPos, Vector3.down, out slopeHit, cCollider.height + 0.01f, groundMask))
        {
            if (slopeHit.normal != Vector3.up)
                return true;
            else
                return false;
        }
        return false;
    }

    /// <summary>
    /// Adding the 6DOF by changing the position of the collider based on where the player is in real space.
    /// </summary>
    private void FollowHeadset()
    {
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        meshObstacle.height = rig.cameraInRigSpaceHeight;
        Vector3 offset = new Vector3(capsuleCenter.x, cCollider.height / 2, capsuleCenter.z);
        meshObstacle.center = offset;
        cCollider.center = offset;
    }

}