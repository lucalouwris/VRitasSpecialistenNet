using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomForce : MonoBehaviour
{
    private float interval = 4;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float forceToApply = 90;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time % interval < 0.01f)
        {
            Debug.Log("matches 0");
            rigidbody.AddForce(Random.insideUnitSphere * forceToApply, ForceMode.Impulse);
        }
    }
}
