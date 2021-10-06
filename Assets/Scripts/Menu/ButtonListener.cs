using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListener : MonoBehaviour
{
    MenuController MenuControl;
    [SerializeField] Text Title;
    [HideInInspector] public int envNumber;
    [HideInInspector] public string envName;
    private void Start()
    {
        MenuControl = GameObject.Find("MenuController").GetComponent<MenuController>();
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            MenuControl.SwitchScene(envNumber);
        });
        Title.text = envName;
    }
}
