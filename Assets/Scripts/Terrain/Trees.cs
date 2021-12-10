using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    void Start()
    {
        Quaternion target = Quaternion.Euler(0, Random.Range(-180f, 180f), 0);
        this.transform.rotation = target;
    }
}
