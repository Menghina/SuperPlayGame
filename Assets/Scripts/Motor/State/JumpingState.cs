using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : BaseState
{
    public override void Construct()
    {
        base.Construct();
        motor.VerticalVelocity = motor.JumpForce;
    }

    public override Vector3 ProcessMotion(Vector3 input)
    {
        ApplySpeed(ref input, motor.MovementSpeed);
        ApplyGravity(ref input, motor.Gravity);

        return input;
    }

    public override void Transition()
    {
        // change back to falling state when the jump height has ended
        if (motor.VerticalVelocity < 0.0f)
            motor.ChangeState("FallingState");
    }
}
