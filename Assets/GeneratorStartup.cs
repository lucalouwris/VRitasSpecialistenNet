using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorStartup : MonoBehaviour
{

    [SerializeField] private MeshRenderer objectRenderer;
    [SerializeField] private Material wantedMaterialPrimary;
    [SerializeField] private Material wantedMaterialSecondary;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    //This method changes the material of the middle power
    public void activateGenerator()
    {
        Debug.Log("Updating Generator Materials");
        // get the current array of materials
        var materials = objectRenderer.materials;
        // exchange both materials with the activated material version // numbers are switched because unity is weird
        materials[1] = wantedMaterialSecondary;
        materials[2] = wantedMaterialPrimary;

        // reassign the materials to the renderer
        objectRenderer.materials = materials;

    }

}
