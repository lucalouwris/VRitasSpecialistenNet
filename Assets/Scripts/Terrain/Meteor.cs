using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private float range;

   void Start()
    {
        range = Random.Range(0.35f, 2.5f);
    }

    void Update()
    {
       
        if (this.range > 0.8f)
        {
            this.transform.Rotate(range * (Time.time * 0.001f), 0, 0);
        } else
        {
            this.transform.Rotate(0, range * (Time.time * 0.001f), 0);
        }
    }
}
