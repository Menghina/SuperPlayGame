using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TO DO eliminate the enheritance
public class AIWalkingState : WalkingState
{
    public override void Transition()
    {
        if (!motor.Grounded())
        {
            motor.ChangeState("FallingState");
        }
    }
}
