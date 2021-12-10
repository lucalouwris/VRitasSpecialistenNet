using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseOverNotif : MonoBehaviour
{
    ComputerUI ui;
    private void OnTriggerEnter(Collider other)
    {
        ui = GameObject.Find("Terminal").GetComponent<ComputerUI>();
        ui.Wipe();
    }
}
