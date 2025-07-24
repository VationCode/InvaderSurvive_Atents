using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxeState : UpperBaseState
{
    float T;
    public override void EnterState(UpperStateManager aim)
    {
        if (PlayerMovementManager.isAll) return;

        aim.gun.gameObject.SetActive(false);
        aim.pickAxe.gameObject.SetActive(true);
        PlayerAnimationManager.Instance.animator.SetBool("IsAiming", false);
        PlayerAnimationManager.Instance.animator.SetLayerWeight(1, 1);
        UpperStateManager.multiAimConstraintData[0].weight = 1f;
        UpperStateManager.multiAimConstraintData[1].weight = 0f;
        UpperStateManager.multiAimConstraintData[2].weight = 0f;
        aim.currentFov = aim.IdleFov;
    }

    public override void UpdateState(UpperStateManager aim)
    {
        if (PlayerMovementManager.isAll) return;

    }
}
