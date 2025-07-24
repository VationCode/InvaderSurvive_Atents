//**********22.12.28 : ���׿� �ٸ� �ִϸ��̼� ���� �̰����� ������ �Ͼ�⿡ Idle���·� ������ �� ���̾ ������ - ����ȣ 
//**********RifleIDleState : �߻� ������ Ŭ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleIDleState : UpperBaseState
{
    public override void EnterState(UpperStateManager aim)
    {
        if (PlayerMovementManager.isAll) return;
    
        PlayerAnimationManager.Instance.animator.SetBool("IsAiming", false);
        PlayerAnimationManager.Instance.animator.SetLayerWeight(1, 0);
        UpperStateManager.multiAimConstraintData[0].weight = 1f;
        UpperStateManager.multiAimConstraintData[1].weight = 0f;
        UpperStateManager.multiAimConstraintData[2].weight = 0f;
        aim.currentFov = aim.IdleFov;
    }

    public override void UpdateState(UpperStateManager aim) 
    {
        if (PlayerMovementManager.isAll) return;

        else if (PlayerInput.Instance.isAttack) aim.SwitchState(aim.Aim);
        if (PlayerInput.Instance.isReload) aim.SwitchState(aim.reload);
        else if (PlayerInput.Instance.isPickAxe)
        {
            aim.SwitchState(aim.pickAxeState);
        }
        
    }
}
