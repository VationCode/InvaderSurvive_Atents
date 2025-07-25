//**********22.12.31 : 카메라 문제 해결완료 - 허인호
//**********22.12.27 : 이동문제(뒤로 회피기 사용 시 가만히 있는 문제 발생) 해결 완료, 카메라 문제 진행중 - 허인호
//**********22.12.26 : 회피 애니메이션 동작 이동 구현(VirtualCamera 및 이동에 관한 문제점 발생)- 허인호
//**********PlayerDodgeState : 회피기 상태 클래스
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : AllBodyBaseState
{
    float t;
    public override void EnterState(PlayerMovementManager movement)
    {
        movement.currentMoveSpeed = 0f;
        Quaternion rotation = Quaternion.identity;
        if (movement.moveDirection == Vector3.zero)
        {
            rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
        else if (movement.moveDirection != Vector3.zero)
        {
            rotation = Quaternion.LookRotation(movement.moveDirection);
        }
        movement.modelObj.transform.rotation = rotation;
        movement.isDodge = true;
        PlayerMovementManager.isAll = true;
        PlayerAnimationManager.Instance.animator.SetTrigger("Dodge");
        PlayerAnimationManager.Instance.animator.SetLayerWeight(1, 0);
        UpperStateManager.multiAimConstraintData[0].weight = 0f;
        UpperStateManager.multiAimConstraintData[1].weight = 0f;
        UpperStateManager.multiAimConstraintData[2].weight = 0f;
    }

    public override void UpdateState(PlayerMovementManager movement)
    {
        movement.currentMoveSpeed = 0f;
        t += Time.deltaTime;
        if (t > movement.dodgeT)
        {
            var targetRotation = Camera.main.transform.eulerAngles.y;
            movement.modelObj.transform.eulerAngles = Vector3.up * targetRotation;

            movement.SwitchState(movement.run);
            PlayerMovementManager.isAll = false;
            movement.isDodge = false;
            t = 0f;
        }
    }
}
