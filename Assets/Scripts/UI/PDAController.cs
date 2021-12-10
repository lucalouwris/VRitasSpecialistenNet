using UnityEngine;

public class PDAController : MonoBehaviour
{
    [SerializeField] private Transform clockHandTransform;
    private float currRotation;
    private float rotation = 90;     //90 degrees a secont
    public float rotationSpeed = 5; //Number of seconds it takes to complete 90 degrees
    private float resetTime = 20;
    private float countdownTime;
    //private float test = 20;
    private bool Moving = true;

    private void Start()
    {
        countdownTime = 600;
    }

    private void Update()
    {
        currRotation = clockHandTransform.localEulerAngles.y;

        if (currRotation <= 315 && currRotation > 180) //Empty
        {
            if (Moving)
            {
                rotation = 90;
                rotationSpeed = resetTime;
            }
            else //Rotation set to 0, waiting for call for ResetWatch
            {
                rotation = 0;
            }
            
        } else if (currRotation > 45 && currRotation < 180) //On full
        {
            rotation = -90;                                 //Change the direction of the rotation
            rotationSpeed = countdownTime;                  //Change the speed to the count down time
            Moving = false;                                 //Set the moving to false so the arrow stops when its empty
        }


        clockHandTransform.localEulerAngles += new Vector3(0, Time.deltaTime * rotation / rotationSpeed, 0);

        //This is just to test that mu function works
        /*Debug.Log(rotationSpeed);
        test -= Time.deltaTime;
        if (test < 0)
        {
            test = 30;
            ResetClock(10);
        }*/
        
    }

    public void ResetClock(float timeUntilEmpty)
    {
        countdownTime = timeUntilEmpty;
        rotationSpeed = resetTime;
        Moving = true;
    }
}
