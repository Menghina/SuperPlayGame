using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFallingState : FallingState
{
    public override void Transition()
    {
        if (motor.Grounded())
        {
            motor.ChangeState("AIWalkingState");
        }
    }
}
