//**********23.01.05 : ź ���� ���� ������ �Ұ� - ����ȣ
//**********23.01.02 : �ѱ� ���� �ÿ��� ����������(���� �Ѿ˻��¿� ���� ���鵵 ���� �ʿ�, �̿ϼ� �۾���) - ����ȣ
//**********22.12.28 : �������ϰ� �۾�x - ����ȣ 
//**********ReloadState : ������ ���� Ŭ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : UpperBaseState
{
    float t = 0f;
    bool isReload;
    public override void EnterState(UpperStateManager aim)
    {
        if (PlayerMovementManager.isAll || isReload) //�������� �� ���� ����
            aim.SwitchState(aim.RifleIdle);
        if (aim.gun.gunInfo.ammoRemain <= 0 || aim.gun.gunInfo.magAmmo >= aim.gun.gunInfo.magCapacity)
        {
            aim.SwitchState(aim.RifleIdle);
            return;
        }        

        isReload = true;
        //���ǹ� �ɾ�ΰ� ������ �� �ְ�
        PlayerAnimationManager.Instance.animator.SetTrigger("Reload");
        PlayerAnimationManager.Instance.animator.SetLayerWeight(1, 1);
        UpperStateManager.multiAimConstraintData[0].weight = 1f;
        UpperStateManager.multiAimConstraintData[1].weight = 1f;
        UpperStateManager.multiAimConstraintData[2].weight = 1f;

        aim.gun.Reload();
    }

    public override void UpdateState(UpperStateManager aim)
    {
        t += Time.deltaTime;
        if (t > aim.reloadT)
        {
            isReload = false;
            if(PlayerInput.Instance.isAttack) aim.SwitchState(aim.Aim);
            else aim.SwitchState(aim.RifleIdle);
            t = 0f;
        }

    }
}
