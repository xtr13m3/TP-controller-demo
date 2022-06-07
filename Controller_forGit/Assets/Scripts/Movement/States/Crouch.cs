using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MovementBaseState
{
    public override void EnterState(MovementManager movement)
    {
        movement.anim.SetBool("Crouch", true);
    }

    public override void UpdateState(MovementManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift)) ExitState(movement, movement.runState);
        if(Input.GetKeyDown(KeyCode.C))
        {
            if (movement.direction.magnitude < 0.1f) ExitState(movement, movement.idleState);
            else ExitState(movement, movement.walkState);
        }

        if (movement.vertical < 0) movement.curSpeed = movement.crouchedWalkBackSpeed;
        else movement.curSpeed = movement.crouchedWalkSpeed;
    }

    void ExitState(MovementManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("Crouch", false);
        movement.SwitchState(state);
    }
}
