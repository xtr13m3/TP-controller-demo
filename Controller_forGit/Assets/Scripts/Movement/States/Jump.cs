using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MovementBaseState
{
    public override void EnterState(MovementManager movement)
    {
        if (movement.prevState == movement.idleState) movement.anim.SetTrigger("IdleJump");
        else if (movement.prevState == movement.walkState || movement.prevState == movement.runState) movement.anim.SetTrigger("RunJump");
    }

    public override void UpdateState(MovementManager movement)
    {
        if(movement.jumped == true && movement.IsGrounded())
        {
            movement.jumped = false;
            if (movement.horizontal == 0 && movement.vertical == 0) movement.SwitchState(movement.idleState);
            else if (Input.GetKey(KeyCode.LeftShift)) movement.SwitchState(movement.runState);
            else movement.SwitchState(movement.walkState);
        }
    }
}
