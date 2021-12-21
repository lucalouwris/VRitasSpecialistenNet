/*
    The Walking Controller gets the distance both controllers travel when you move them with Vector3.Distance. 
    If one or both the controllers report their isWalking equals to true, the player rigidbody get a velocity based on the combined distance of the left and right controller.
*/
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
        // Calculate distance travelled of left controller.
        leftController.previousPosition = leftController.transform.position;
        leftController.c_Movement = (Vector3.Distance(leftController.currentPosition, leftController.previousPosition) / Time.deltaTime) / 100;
        // Calculate distance travelled of right controller.
        rightController.previousPosition = rightController.transform.position;
        rightController.c_Movement = (Vector3.Distance(rightController.currentPosition, rightController.previousPosition) / Time.deltaTime) / 100;

        if (leftController.isWalking || rightController.isWalking) 
        {
            // If either of the controllers has their grip button pressed. Use the distance travelled to move.
            Vector3 direction = Camera.main.transform.forward;
            direction.y = 0;

            float moveDistance = leftController.c_Movement + rightController.c_Movement;
            playerBody.velocity += (direction * moveDistance) * speedModifier;
        }
        else
        {
            // Else if no grips are pressed.
            playerBody.velocity = new Vector3(0, playerBody.velocity.y, 0);
        }
        leftController.currentPosition = leftController.transform.position;
        rightController.currentPosition = rightController.transform.position;
    }
}
