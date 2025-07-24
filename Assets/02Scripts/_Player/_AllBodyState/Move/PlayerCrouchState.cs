//**********22.12.24 : ���� �ִϸ��̼� ���۰� �ȴ� �ӵ� ���� - ����ȣ
//**********PlayerCrouchState : �ɱ� ���� Ŭ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : AllBodyBaseState
{
    public override void EnterState(PlayerMovementManager movement)
    {
        PlayerAnimationManager.Instance.animator.SetBool("IsCrouching", true);
    }

    public override void UpdateState(PlayerMovementManager movement)
    {
        if (movement.isDead)
        {
            ExitState(movement, movement.death);
            return;
        }
        if (PlayerInput.Instance.isCrouch)
        {
            //if (movement.moveDirection.magnitude < 0.1f) ExitState(movement, movement.idle);
            ExitState(movement, movement.run);
        }
        else if (PlayerInput.Instance.isDodge) ExitState(movement, movement.dodge);
        else if (PlayerInput.Instance.isSkillE) ExitState(movement, movement.skillE);
        else if (PlayerInput.Instance.isJump)
        {
            movement.previousState = this;
            ExitState(movement, movement.jump);
        }
       // else if () ExitState(movement, movement.death); //�׾������� ����
        movement.currentMoveSpeed = movement.crouchSeed;
        PlayerAnimationManager.Instance.Move(PlayerInput.Instance.moveInput.x, PlayerInput.Instance.moveInput.y);
    }

    void ExitState(PlayerMovementManager movement, AllBodyBaseState state) //��ȯ�Ϸ����ϴ� ���¸� �𸥴ٴ� �������� �Լ��� ȣ��
    {
        PlayerAnimationManager.Instance.animator.SetBool("IsCrouching", false);

        movement.SwitchState(state);
    }
}
