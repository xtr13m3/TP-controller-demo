using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimBaseState 
{
    public abstract void EnterState(AimManager aim);
    public abstract void UpdateState(AimManager aim);
}
