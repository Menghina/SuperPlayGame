using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : BaseState
{
    public override Vector3 ProcessMotion(Vector3 input)
    {
        ApplyGravity(ref input, motor.MovementSpeed);
        ApplySpeed(ref input, motor.MovementSpeed);

        return input;
    }

    public override void Transition()
    {
        if (motor.Grounded())
        {
            motor.ChangeState("WalkingState");
        }
    }
}
