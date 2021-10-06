using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class VRInput : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    Color bNormal, bHighlighted, bClicked;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
        bNormal = new Color(113, 5, 167);
        bHighlighted = new Color(235,114, 187);
        bClicked = new Color(157, 60, 108);
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.tag == "Button")
        {
            Debug.Log(e.target.name + "Clicked");
            var bColor = e.target.GetComponent<Button>();
            bColor.image.CrossFadeColor(bColor.colors.pressedColor, bColor.colors.fadeDuration, true, true);
            bColor.onClick.Invoke();
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.tag == "Button")
        {
            Debug.Log(e.target.name + "Entered");
            var bColor = e.target.GetComponent<Button>();
            bColor.image.CrossFadeColor(bColor.colors.highlightedColor, bColor.colors.fadeDuration, true, true);
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.tag == "Button")
        {
            Debug.Log(e.target.name + "Exited");
            var bColor = e.target.GetComponent<Button>();
            bColor.image.CrossFadeColor(bColor.colors.normalColor, bColor.colors.fadeDuration, true, true);
        }
    }
}