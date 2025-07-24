//**********22.12.26 : �ִϸ��̼Ǹ� �־���� ���� - ����ȣ
//**********PlayerSkillEState : SkillE ���� Ŭ����(���� ��ų)
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
