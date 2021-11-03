using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Mathf.Sin(Random.Range(0.35f, 5.0f) * Time.time), Mathf.Sin(Random.Range(0.35f, 5.0f) * Time.time), Mathf.Sin(Random.Range(0.35f, 5.0f) * Time.time));
    }
}
