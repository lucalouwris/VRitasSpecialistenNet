using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewdriverTrigger : MonoBehaviour
{
    public string tagToCheck;
    private GameObject powerCell;
    private bool hasbeenFixed = false;

    [SerializeField] private MeshRenderer objectRenderer;
    [SerializeField] private Material wantedMaterial;

    // Start is called before the first frame update
    void Start()
    {
        powerCell = GameObject.Find("powercellempty");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This method checks if the colliding obejct has a specific tag
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision deteced; checking for this tag:" + tagToCheck);
        if (other.gameObject.CompareTag(tagToCheck))
        {
            if (!hasbeenFixed)
            {
                TransformPowerCell();
                changeMaterial();
            }
        }
    }

    //Transforms the Powercell into the "repaired" position
    private void TransformPowerCell()
    {
        Debug.Log("Trying to apply Rotation and Transform");
        powerCell.transform.Rotate(0f, 0f, 7f, Space.World);

        GetComponent<SwitchCheck>().SwitchShouldWork = true;

        hasbeenFixed = true;

    }

    //Need this for the lever to check if the machine has been fixed or nah
    public bool getHasBeenFixed()
    {
        return hasbeenFixed;
    }

    private void changeMaterial()
    {
        Debug.Log("Trying to update the powercell materials");
        // get the current array of materials
        var materials = objectRenderer.materials;
        // exchange one material
        materials[1] = wantedMaterial;
        // reassign the materials to the renderer
        objectRenderer.materials = materials;

    }
}