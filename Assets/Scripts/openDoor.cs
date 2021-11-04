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
            if (transform.position.x < -6)
            {
                transform.position = new Vector3(transform.position.x + 0.03f, transform.position.y, transform.position.z);
            }
        }
    }
}
