using UnityEngine;

public class PDAController : MonoBehaviour
{
    [SerializeField] private Transform clockHandTransform;
    public float currRotation;
    private float rotation;
    public static float rotationSpeed = 1f;


    private void Update()
    {
        currRotation = clockHandTransform.rotation.eulerAngles.z;
        Debug.Log(currRotation);
        
        if (currRotation <= 315 && currRotation > 180)
        {
            rotation = 90f;
        }
        else if (currRotation > 45 && currRotation < 180)
        {
            rotation = -45f;
        }

        clockHandTransform.eulerAngles += new Vector3(0, 0, (Time.deltaTime * rotation) / rotationSpeed);
        
    }


    public static void SetTime(float inTime)
    {
        rotationSpeed = inTime;
    }
    
}
