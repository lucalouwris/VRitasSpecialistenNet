using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MovementController : MonoBehaviour
{
    [SerializeField] Transform PointerTarget;
    [SerializeField] Rig LookAt;
    [SerializeField] Rig PointAt;
    [SerializeField] float transition = 0.01f;
    private bool LookShouldIncrease = true;
    private bool PointShouldIncrease = false;

    public void ChangePoint(Vector3 newPosition)
    {
        //Change the position of the target to point at
        PointerTarget.position = newPosition;
    }

    public void ResetRigWeight(Rig rig)
    {
        //Sets the weight of the pointing to 0 the arm just runs the animation        
        if (rig == PointAt)
        {
            PointShouldIncrease = false;           
        } else if (rig == LookAt)
        {
            LookShouldIncrease = false;
        }
    }

    public void SetRigWeight(Rig rig)
    {
        //Sets the weight of the pointing to 1        
        if(rig == PointAt)
        {
            PointShouldIncrease = true;
        } else if (rig == LookAt)
        {
            LookShouldIncrease = true;
        }
    }

    private void Update()
    {
        if (PointShouldIncrease && PointAt.weight < 1)
        {
            PointAt.weight += transition*Time.deltaTime;
        } else if (!PointShouldIncrease && PointAt.weight > 0)
        {
            PointAt.weight -= transition * Time.deltaTime;
        }


        if (LookShouldIncrease && LookAt.weight < 1)
        {
            LookAt.weight += transition * Time.deltaTime;
        }
        else if (!LookShouldIncrease && LookAt.weight > 0)
        {
            LookAt.weight -= transition * Time.deltaTime;
        }
    }

    // Update is called once per frame

    [ContextMenu("Change Point")]
    void RandomPoint()
    {
        ChangePoint(Random.onUnitSphere);
    }

    [ContextMenu("Set Pointing")]
    void Point()
    {
        SetRigWeight(PointAt);
    }

    [ContextMenu("Clear Pointing")]
    void RemovePoint()
    {
        ResetRigWeight(PointAt);
    }

    [ContextMenu("Set looking")]
    void Look()
    {
        SetRigWeight(LookAt);
    }
    
    [ContextMenu("Clear looking")]
    void RemoveLook()
    {
        ResetRigWeight(LookAt);
    }
}
