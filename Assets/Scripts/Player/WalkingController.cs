using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingController : MonoBehaviour
{
    [SerializeField] ControllerInput leftController;
    [SerializeField] ControllerInput rightController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (leftController.isWalking || rightController.isWalking)
        {
            leftController.previousPosition = leftController.transform.position;
            leftController.c_Movement = (Vector3.Distance(leftController.currentPosition, leftController.previousPosition) / Time.deltaTime) / 100;

            rightController.previousPosition = rightController.transform.position;
            rightController.c_Movement = (Vector3.Distance(rightController.currentPosition, rightController.previousPosition) / Time.deltaTime) / 100;

            Vector3 direction = Camera.main.transform.forward;
            direction.y = 0;

            float moveDistance = leftController.c_Movement + rightController.c_Movement;
            transform.position += (direction * moveDistance);

            leftController.currentPosition = leftController.transform.position;
            rightController.currentPosition = rightController.transform.position;
        }
    }
}
