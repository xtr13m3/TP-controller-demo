using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingState : AimBaseState
{
    public override void EnterState(AimManager aim)
    {
        aim.anim.SetBool("Aim", true);
        aim.curFov = aim.aimFov;
    }

    public override void UpdateState(AimManager aim)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1)) aim.SwitchState(aim.aimIdle);
    }
}
