//**********23.01.05 : 탄 수에 따른 재장전 불가 - 허인호
//**********23.01.02 : 총기 상태 시에만 재장전가능(차후 총알상태에 따른 값들도 지정 필요, 미완성 작업중) - 허인호
//**********22.12.28 : 생성만하고 작업x - 허인호 
//**********ReloadState : 재장전 상태 클래스
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : UpperBaseState
{
    float t = 0f;
    bool isReload;
    public override void EnterState(UpperStateManager aim)
    {
        if (PlayerMovementManager.isAll || isReload) //재장전할 수 없는 상태
            aim.SwitchState(aim.RifleIdle);
        if (aim.gun.gunInfo.ammoRemain <= 0 || aim.gun.gunInfo.magAmmo >= aim.gun.gunInfo.magCapacity)
        {
            aim.SwitchState(aim.RifleIdle);
            return;
        }        

        isReload = true;
        //조건문 걸어두고 진입할 수 있게
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
