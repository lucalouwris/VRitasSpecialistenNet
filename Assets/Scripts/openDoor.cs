using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(transform.localRotation.eulerAngles.x > 10)
        {
            transform.Rotate(new Vector3(0, 15, 0) * Time.deltaTime);
        }
        else
        {
            if (transform.localPosition.x < -10.5f)
            {
                transform.position = new Vector3(transform.localPosition.x + 1f * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }
    }
}
