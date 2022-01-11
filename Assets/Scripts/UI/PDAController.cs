using UnityEngine;

public class PDAController : MonoBehaviour
{
    [SerializeField] private Transform clockHandTransform;
    private float currRotation;
    private float rotation = 90;     //90 degrees a second
    [SerializeField] public float rotationSpeed = 600; //Number of seconds it takes to complete 90 degrees
   

    private void Update()
    {
        currRotation = clockHandTransform.localEulerAngles.y;

        if (!(currRotation <= 325 && currRotation > 180))
        {
            clockHandTransform.localEulerAngles -= new Vector3(0, Time.deltaTime * rotation / rotationSpeed, 0);
        } 
    }

    public void fillOxygen()
    {
        clockHandTransform.localEulerAngles = new Vector3(0, 45, 0);
    }

    [ContextMenu("Fill oxygen")]
    void ResetFromInspector()
    {
        fillOxygen();
    }
}
