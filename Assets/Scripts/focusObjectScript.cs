using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class focusObjectScript : MonoBehaviour
{

    public float amplitude;
    public float frequency;

    private float x;
    private float y;
    private float z;

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {

        this.HandlePattern();

        transform.position = new Vector3(x, y, z);
    }

    void ReversedLinearPattern()
    {
        x = Mathf.Sin(Time.time * frequency) * amplitude;
        y = transform.position.y;
        z = transform.position.z;
    }

    void LinearPattern()
    {
        x = transform.position.x;
        y = Mathf.Sin(Time.time * frequency) * amplitude;
    }

    void CirclePattern()
    {
        x = Mathf.Sin(Time.time * frequency) * amplitude;
        y = Mathf.Cos(Time.time * frequency) * amplitude;
    }

    void KnotPattern()
    {
        x = Mathf.Sin(Time.time * frequency) * amplitude;
        y = Mathf.Cos(Time.time * frequency) * Mathf.Sin(Time.time * frequency) * amplitude;
    }

    void ReveredKnotPattern()
    {
        x = Mathf.Cos(Time.time * frequency) * Mathf.Sin(Time.time * frequency) * amplitude;
        y = Mathf.Sin(Time.time * frequency) * amplitude;
    }

    void WeirdPattern()
    {
        x = Mathf.Sin(Time.time * frequency) * amplitude;
        y = Mathf.Cos(Time.time * frequency) * Mathf.Sin(Time.time * frequency) * amplitude;
    }

    void HandlePattern()
    {

        if (Time.time < 10.0)
        {
            this.ReversedLinearPattern();
        }

        if (Time.time > 10.0 && Time.time < 30.0)
        {
            this.LinearPattern();
        }

        if (Time.time > 30.0 && Time.time < 40.0)
        {
            this.CirclePattern();
        }


        if (Time.time > 40.0 && Time.time < 50.0)
        {
            this.KnotPattern();
        }


        if (Time.time > 50.0 && Time.time < 60.0)
        {
            this.ReveredKnotPattern();
        }

        if (Time.time > 60.0 && Time.time < 70.0)
        {
            this.WeirdPattern();
        }
    }
}
