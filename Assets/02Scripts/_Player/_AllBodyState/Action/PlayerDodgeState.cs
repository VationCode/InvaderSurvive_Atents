//**********22.12.31 : ī�޶� ���� �ذ�Ϸ� - ����ȣ
//**********22.12.27 : �̵�����(�ڷ� ȸ�Ǳ� ��� �� ������ �ִ� ���� �߻�) �ذ� �Ϸ�, ī�޶� ���� ������ - ����ȣ
//**********22.12.26 : ȸ�� �ִϸ��̼� ���� �̵� ����(VirtualCamera �� �̵��� ���� ������ �߻�)- ����ȣ
//**********PlayerDodgeState : ȸ�Ǳ� ���� Ŭ����
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
