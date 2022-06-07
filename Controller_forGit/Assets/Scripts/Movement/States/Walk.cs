using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MovementBaseState
{
    public override void EnterState(MovementManager movement)
    {
        movement.anim.SetBool("Walk", true);
    }

    public override void UpdateState(MovementManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift)) ExitState(movement, movement.runState);
        else if (Input.GetKeyDown(KeyCode.C)) ExitState(movement, movement.crouchState);
        else if (movement.direction.magnitude < 0.1f) ExitState(movement, movement.idleState);

        if (movement.vertical < 0) movement.curSpeed = movement.walkBackSpeed;
        else movement.curSpeed = movement.walkSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.prevState = this;
            ExitState(movement, movement.jumpState);
        }
    }

    void ExitState(MovementManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("Walk", false);
        movement.SwitchState(state);
    }
}
