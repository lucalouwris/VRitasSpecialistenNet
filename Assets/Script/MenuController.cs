using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void StartProgram()
    {
        Destroy(MainMenu);
    }
    public void ExitProgram()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
