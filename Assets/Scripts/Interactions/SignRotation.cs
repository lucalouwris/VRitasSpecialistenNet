using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignRotation : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("XR Rig");
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            float distance = Vector3.Distance(this.transform.position, player.transform.position);
            if (distance < 1)
            {
                Vector3 targetPostition = new Vector3(-player.transform.position.x,
                                           this.transform.position.y,
                                           -player.transform.position.z);
                this.transform.LookAt(targetPostition);
            }
        }
    }
}
