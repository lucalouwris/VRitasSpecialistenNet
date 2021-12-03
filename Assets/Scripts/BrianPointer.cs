using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrianPointer : MonoBehaviour
{
    public Image img;
    public Transform target;
    public Vector3 offset;
    [SerializeField] Camera cam;
    [SerializeField] float blinkInterval = 0.5f;
    float currentInterval;

    private void Start()
    {
        currentInterval = blinkInterval;
    }

    private void Update()
    {
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = cam.WorldToScreenPoint(target.position + offset);
        float distance = Vector3.Distance(target.position, transform.position);

        img.rectTransform.localScale = new Vector3(1f / (distance / 2f), 1f / (distance / 2f), 1f);
        if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
                pos.x = maxX;
            else
                pos.x = minX;
            pos.y = minY;
        }
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;

        if (currentInterval > 0)
            currentInterval -= Time.deltaTime;
        else
        {
            if (img.color.a == 1f)
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
            else
                img.color = new Color(img.color.r, img.color.g, img.color.b, 1);

            currentInterval = blinkInterval;
        }
    }
}
