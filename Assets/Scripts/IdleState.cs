using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AimBaseState
{
    public override void EnterState(AimStatesController control)
    {
        control.NewRightHandWeight = 0;
        control.NewRigWeight = 0;
        control.CurrentOffsetPosition = control.IdleOffsetPosition;
    }

    public override void FixedState(AimStatesController control)
    {

    }

    public override void UpdateState(AimStatesController control)
    {
        if (control.InputControl.actions["Aim"].IsPressed() && control.CurrentGun)
        {
            ExitState(control, control.Aim);
        }
    }

    public override void ExitState(AimStatesController control, AimBaseState newState)
    {
        base.ExitState(control, newState);
    }
}
