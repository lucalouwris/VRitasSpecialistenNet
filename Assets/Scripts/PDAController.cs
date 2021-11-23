using UnityEngine;

public class PDAController : MonoBehaviour
{
    [SerializeField] private Transform clockHandTransform;
    private float currRotation;
    private float rotation = 90;
    public static float rotationSpeed = 1f;


    private void Update()
    {
        currRotation = clockHandTransform.localEulerAngles.z;

        if ((currRotation <= 315 && currRotation > 180) || (currRotation > 45 && currRotation < 180))
        {
            rotation *= -1;        
        }

        clockHandTransform.localEulerAngles += new Vector3(0, 0, (Time.deltaTime * rotation) / rotationSpeed);
    }


    public static void SetTime(float inTime)
    {
        rotationSpeed = inTime;
    }
    
}
