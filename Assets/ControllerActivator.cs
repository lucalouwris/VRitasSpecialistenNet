using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerActivator : MonoBehaviour
{
    [SerializeField] GameObject controller;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Tryin to enable" + controller);
            controller.SetActive(true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Tryin to disable" + controller);
            controller.SetActive(false);
        }
    }
}
