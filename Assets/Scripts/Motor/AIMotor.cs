using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMotor : BaseMotor
{
    private Vector3 destination = Vector3.zero;

    protected override void Start()
    {
        base.Start();
        //add camera 

        state = gameObject.AddComponent<AIWalkingState>();
        state.Construct();
    }
    protected override void UpdateMotor()
    {
        //get the movement input for player
        MoveVector = Direction();

        RotationQuaternion = state.ProcessRotation(MoveVector);
        MoveVector = state.ProcessMotion(MoveVector);
        // check if we need to change current state
        state.Transition();
        Move();
        //Rotate();
    }

    public void SetDestination(Transform t)
    {
        destination = t.position;
    }

    public Vector3 Direction()
    {

        if (destination == Vector3.zero)
        {
            return destination;
        }
        Vector3 dir = destination - thisTransform.position;
        dir.Set(dir.x, 0, dir.z);

        return dir.normalized;
    }

}
