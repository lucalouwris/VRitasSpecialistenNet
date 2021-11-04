using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignRotation : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float rotateDistance = 25;
    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            float distance = Vector3.Distance(this.transform.position, player.transform.position);
            if (distance < rotateDistance)
            {
                Vector3 targetPostition = new Vector3(player.transform.position.x,
                                           this.transform.position.y,
                                           player.transform.position.z);
                this.transform.LookAt(targetPostition);
            }
        }
    }
}
