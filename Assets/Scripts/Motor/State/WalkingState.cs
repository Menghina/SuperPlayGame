using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : BaseState
{

    public override void Construct()
    {
        base.Construct();
        motor.VerticalVelocity = 0;
    }
    public override Vector3 ProcessMotion(Vector3 input)
    {
        ApplySpeed(ref input, motor.MovementSpeed);
        return input;
    }

    public override void Transition()
    {
        if (!motor.Grounded())
        {
            motor.ChangeState("FallingState");
        }

        if (Input.GetButtonDown("Jump"))
        {
            motor.ChangeState("JumpingState");
        }
    }
}
