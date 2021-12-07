using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class BrianPointer : MonoBehaviour
{
    public GameObject pointer;
    public Material mat;
    public GameObject target;
    public Vector3 offset;
    [SerializeField] Camera cam;
    [HideInInspector] public bool blink;
    [SerializeField] float blinkInterval = 0.5f;
    float currentInterval;

    private void Start()
    {
        blink = true;
        currentInterval = blinkInterval;
    }

    private void Update()
    {
        if (blink)
        {
            if (!pointer.activeSelf)
                pointer.SetActive(true);

            Vector3 relativePos = target.transform.position - transform.position;
            Quaternion LookAtRotation = Quaternion.LookRotation(relativePos);

            Quaternion LookAtRotationXY = Quaternion.Euler(LookAtRotation.eulerAngles.x, LookAtRotation.eulerAngles.y, 0f);

            pointer.transform.rotation = LookAtRotationXY;

                if (currentInterval > 0)
                    currentInterval -= Time.deltaTime;
                else
                {
                    if (mat.color.a == 1f)
                        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0);
                    else
                        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1);

                    currentInterval = blinkInterval;
                }
        }
        else
            pointer.SetActive(false);
    }
}
