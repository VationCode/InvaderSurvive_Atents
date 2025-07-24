//**********22.12.28 : 리그와 다른 애니메이션 동작 이것저것 간섭이 일어나기에 Idle상태로 들어왔을 시 레이어를 꺼버림 - 허인호 
//**********RifleIDleState : 발사 대기상태 클래스
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
