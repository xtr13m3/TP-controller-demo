using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionBaseState 
{
    public abstract void EnterState(ActionsManager actions);
    public abstract void UpdateState(ActionsManager actions);
}
