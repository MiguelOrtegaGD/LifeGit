using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : AimBaseState
{
    public override void EnterState(AimStatesController control)
    {
        control.NewRightHandWeight = 1;
        control.NewRigWeight = 1;
        control.Aiming = true;
        control.CurrentOffsetPosition = control.AimingOffsetPosition;
    }

    public override void FixedState(AimStatesController control)
    {

    }

    public override void UpdateState(AimStatesController control)
    {
        if (control.InputControl.actions["Aim"].WasReleasedThisFrame())
        {
            ExitState(control, control.Idle);
        }

        if (control.InputControl.actions["Shoot"].WasPressedThisFrame())
        {
            control.CurrentGun.CanShoot(control.AimPosition);
        }
    }

    public override void ExitState(AimStatesController control, AimBaseState newState)
    {
        control.Aiming = false;
        base.ExitState(control, newState);
    }
}
