//**********22.12.26 : Idle ���� ��(�ִϸ����Ϳ��� �޸�����¹ٷ� ��ȯ�̸� �ȿ� Idle�� ����)
//**********22.12.24 : ��� �ִϸ��̼� ����
//**********PlayerRunState : �޸��� ���� Ŭ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : AllBodyBaseState
{
    public override void EnterState(PlayerMovementManager movement)
    {
        
    }

    public override void UpdateState(PlayerMovementManager movement)
    {
        if (movement.moveDirection.magnitude > 0.1f) //0.1f���� ũ��
        {
            if (PlayerInput.Instance.isAttack) movement.SwitchState(movement.walk);
            else movement.SwitchState(movement.run);
        }
        else if (PlayerInput.Instance.isCrouch) movement.SwitchState(movement.crouch);
        else if (PlayerInput.Instance.isDodge) movement.SwitchState(movement.dodge);
        else if (PlayerInput.Instance.isSkillE) movement.SwitchState(movement.skillE);
        else if (PlayerInput.Instance.isJump)
        {
            movement.previousState = this;
            movement.SwitchState(movement.jump);
        }
    }
}
