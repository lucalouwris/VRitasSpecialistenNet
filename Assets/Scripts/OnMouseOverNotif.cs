using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseOverNotif : MonoBehaviour
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Destroy(gameObject);
    }
}
