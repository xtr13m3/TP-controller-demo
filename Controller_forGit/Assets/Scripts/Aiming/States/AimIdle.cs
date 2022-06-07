using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimIdle : AimBaseState
{
    public override void EnterState(AimManager aim)
    {
        aim.anim.SetBool("Aim", false);
        aim.curFov = aim.idleFov;
    }

    public override void UpdateState(AimManager aim)
    {
        if(Input.GetKey(KeyCode.Mouse1)) aim.SwitchState(aim.aimState);
    }
}
