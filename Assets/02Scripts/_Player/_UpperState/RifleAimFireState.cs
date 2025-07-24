//**********22.12.28 : �ѹ߻� �� �ݵ� �ִϸ��̼ǰ� Rig�� ���� ĳ���� ��ü ���� ���󰡱�, ���� ���� �� ���ݴ�� ���� �߰� - ����ȣ 
//**********RifleAimFireState : �߻� ������ Ŭ����
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
        else if (PlayerInput.Instance.isAttack && Time.time >= lastFireTime + aim.gun.gunInfo.fireT) //�߻簡���� �����̰� ���� �ð��� �������߻� �������� �߻簣���� ���Ѱͺ��� �� ���� �ð��� �귶�� ���
        {
            PlayerAnimationManager.Instance.animator.SetTrigger("RifleAttack");
            aim.gun.Fire();
            lastFireTime = Time.time; //������ �ѹ߻� ���� ����ð����� �ʱ�ȭ
        }
        t = 0f;
    }
}
