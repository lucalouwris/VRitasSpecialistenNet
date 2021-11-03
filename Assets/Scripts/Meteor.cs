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

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Mathf.Sin(range * Time.time), 0, 0);
    }
}
