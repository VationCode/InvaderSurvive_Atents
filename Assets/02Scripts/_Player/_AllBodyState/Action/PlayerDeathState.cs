using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : AllBodyBaseState
{
    public override void EnterState(PlayerMovementManager movement)
    {
        PlayerMovementManager.isAll = true;
        PlayerAnimationManager.Instance.animator.SetTrigger("Dead");
    }

    public override void UpdateState(PlayerMovementManager movement)
    {

    }
}
