//**********22.12.28 : 총발사 시 반동 애니메이션과 Rig를 통한 캐릭터 상체 에임 따라가기, 공격 멈춘 후 공격대기 상태 추가 - 허인호 
//**********RifleAimFireState : 발사 대기상태 클래스
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleAimFireState : UpperBaseState
{
    float t = 0f;
    float lastFireTime;
    public override void EnterState(UpperStateManager aim)
    {
        if (PlayerMovementManager.isAll)
        {
            aim.currentFov = aim.IdleFov;
            return;
        }
        PlayerAnimationManager.Instance.animator.SetBool("IsAiming", true);
        PlayerAnimationManager.Instance.animator.SetLayerWeight(1, 1);
        UpperStateManager.multiAimConstraintData[0].weight = 1f;
        UpperStateManager.multiAimConstraintData[1].weight = 1f;
        UpperStateManager.multiAimConstraintData[2].weight = 1f;
        lastFireTime = 0f;
        aim.currentFov = aim.adsFov;
    }

    public override void UpdateState(UpperStateManager aim)
    {
        if (PlayerMovementManager.isAll)
        {
            aim.currentFov = aim.IdleFov;
            return;
        }
        else if (!PlayerMovementManager.isAll)
        {
            PlayerAnimationManager.Instance.animator.SetLayerWeight(1, 1);
            UpperStateManager.multiAimConstraintData[0].weight = 1f;
            UpperStateManager.multiAimConstraintData[1].weight = 1f;
            UpperStateManager.multiAimConstraintData[2].weight = 1f;
            aim.currentFov = aim.adsFov;
        }
        
        if(PlayerInput.Instance.isReload)
        {
            aim.SwitchState(aim.reload);
            t = 0;
        }
        if (!PlayerInput.Instance.isAttack)
        {
            t += Time.deltaTime;
            if (t > aim.aimPoseT)
            {
                PlayerAnimationManager.Instance.animator.SetBool("IsAiming", false);
                aim.SwitchState(aim.RifleIdle);
                t = 0f;
            }
            return;
        }
        if (aim.gun.gunInfo.magAmmo <= 0)
        {
            return;
        }
        else if (PlayerInput.Instance.isAttack && Time.time >= lastFireTime + aim.gun.gunInfo.fireT) //발사가능한 상태이고 현재 시간이 마지막발사 시점에서 발사간격을 더한것보다 더 많은 시간이 흘렀을 경우
        {
            PlayerAnimationManager.Instance.animator.SetTrigger("RifleAttack");
            aim.gun.Fire();
            lastFireTime = Time.time; //마지막 총발사 시점 현재시간으로 초기화
        }
        t = 0f;
    }
}
