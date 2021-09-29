using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class focusObjectScript : MonoBehaviour
{

    public float amplitude;
    public float frequency;

    public float x;
    public float y;
    public float z;

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(x, y, z);

        x = Mathf.Sin(Time.time * frequency) * amplitude;
        y = transform.position.y;
        z = transform.position.z;

        if (Time.time > 40.0)
        {
            x = transform.position.x;
            y = Mathf.Sin(Time.time * frequency) * amplitude;
        }

        if (Time.time > 80.0)
        {
            x = Mathf.Cos(Time.time * frequency) * amplitude; 
            y = Mathf.Sin(Time.time * frequency) * amplitude;
        }
    }
}
