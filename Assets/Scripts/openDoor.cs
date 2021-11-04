using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{
    [SerializeField] GameObject rotated, moved;
    [SerializeField] float rotateSpeed, moveSpeed;
    private void Start()
    {
        StartCoroutine(DoorOpenRotate());
    }

    public IEnumerator DoorOpenRotate()
    {
        float singleStep = rotateSpeed * Time.deltaTime;
        while (transform.rotation != rotated.transform.rotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotated.transform.rotation, singleStep);
            yield return null;
        }
        StartCoroutine(DoorOpenMove());
    }

    public IEnumerator DoorOpenMove()
    {
        float singleStep = moveSpeed * Time.deltaTime;
        while (transform.position != moved.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, moved.transform.position, singleStep);
            yield return null;
        }
    }
}
