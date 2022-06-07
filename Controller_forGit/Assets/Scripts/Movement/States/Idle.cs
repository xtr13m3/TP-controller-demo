using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MovementBaseState
{
    public override void EnterState(MovementManager movement)
    {

    }

    public override void UpdateState(MovementManager movement)
    {
        if(movement.direction.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift)) movement.SwitchState(movement.runState);
            else movement.SwitchState(movement.walkState);
        }

        if (Input.GetKeyDown(KeyCode.C)) movement.SwitchState(movement.crouchState);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            movement.prevState = this;
            movement.SwitchState(movement.jumpState);
        }
    }
}
