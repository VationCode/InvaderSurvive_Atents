//**********22.12.26 : 애니메이션만 넣어놓은 상태 - 허인호
//**********PlayerSkillEState : SkillE 상태 클래스(광역 스킬)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillEState : AllBodyBaseState
{
    float t;
    public override void EnterState(PlayerMovementManager movement)
    {
        movement.currentMoveSpeed = 0f;
        PlayerMovementManager.isAll = true;
        PlayerAnimationManager.Instance.animator.SetTrigger("SkillE");
        PlayerAnimationManager.Instance.animator.SetLayerWeight(1, 0);
        UpperStateManager.multiAimConstraintData[0].weight = 0f;
        UpperStateManager.multiAimConstraintData[1].weight = 0f;
        UpperStateManager.multiAimConstraintData[2].weight = 0f;
        PlayerAnimationManager.Instance.animator.applyRootMotion = true;
    }

    public override void UpdateState(PlayerMovementManager movement)
    {
        if (movement.isDead)
        {
            movement.SwitchState(movement.death);
            return;
        }
        movement.currentMoveSpeed = 0f;
        t += Time.deltaTime;
        if (t > movement.skillET)
        {
            movement.SwitchState(movement.run);
            PlayerAnimationManager.Instance.animator.applyRootMotion = false;
            PlayerMovementManager.isAll = false;
            t = 0f;
        }
    }
}
