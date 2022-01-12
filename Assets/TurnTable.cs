using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTable : MonoBehaviour
{
    public float RotSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * (RotSpeed * Time.deltaTime));
    }
}
