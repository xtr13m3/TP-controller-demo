using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MovementBaseState
{
    public override void EnterState(MovementManager movement)
    {
        movement.anim.SetBool("Run", true);
    }

    public override void UpdateState(MovementManager movement)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift)) ExitState(movement, movement.walkState);
        else if (movement.direction.magnitude < 0.1f) ExitState(movement, movement.idleState);

        if (movement.vertical < 0) movement.curSpeed = movement.runBackSpeed;
        else movement.curSpeed = movement.runSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.prevState = this;
            ExitState(movement, movement.jumpState);
        }
    }

    void ExitState(MovementManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("Run", false);
        movement.SwitchState(state);
    }
}
