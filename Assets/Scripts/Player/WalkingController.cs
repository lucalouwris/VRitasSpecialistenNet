using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingController : MonoBehaviour
{
    [SerializeField] ControllerInput leftController;
    [SerializeField] ControllerInput rightController;
    [SerializeField] Rigidbody playerBody;
    [SerializeField] float speedModifier = 2;

    // Update is called once per frame
    void Update()
    {
        leftController.previousPosition = leftController.transform.position;
        leftController.c_Movement = (Vector3.Distance(leftController.currentPosition, leftController.previousPosition) / Time.deltaTime) / 100;

        rightController.previousPosition = rightController.transform.position;
        rightController.c_Movement = (Vector3.Distance(rightController.currentPosition, rightController.previousPosition) / Time.deltaTime) / 100;

        if (leftController.isWalking || rightController.isWalking)
        {
            Vector3 direction = Camera.main.transform.forward;
            direction.y = 0;

            float moveDistance = leftController.c_Movement + rightController.c_Movement;
            playerBody.velocity += (direction * moveDistance) * speedModifier;
        }
        else
        {
            playerBody.velocity = new Vector3(0, playerBody.velocity.y, 0);
        }
        leftController.currentPosition = leftController.transform.position;
        rightController.currentPosition = rightController.transform.position;
    }
}
