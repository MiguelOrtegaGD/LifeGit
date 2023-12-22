using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimBaseState
{
    public abstract void EnterState(AimStatesController control);
    public abstract void UpdateState(AimStatesController control);
    public abstract void FixedState(AimStatesController control);
    public virtual void ExitState(AimStatesController control, AimBaseState newState)
    {
        control.ChangeState(newState);
    }
}
