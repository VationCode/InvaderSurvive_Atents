//**********22.12.24 : �ȴ� �ִϸ��̼� ���۰� �ȱ� �ӵ� ����, �ȴ� ���´� ���� �߻��� ���� �����̵� - ����ȣ
//**********PlayerWalkState : �ȱ� ���� Ŭ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : AllBodyBaseState
{
    public override void EnterState(PlayerMovementManager movement)
    {
        PlayerAnimationManager.Instance.animator.SetBool("IsWalking", true);
    }

    public override void UpdateState(PlayerMovementManager movement)
    {
        if (movement.isDead)
        {
            ExitState(movement, movement.death);
            return;
        }
        if (!PlayerInput.Instance.isAttack) ExitState(movement, movement.run);
        else if (PlayerInput.Instance.isCrouch) ExitState(movement, movement.crouch);
        //else if (movement.moveDirection.magnitude < 0.1f) ExitState(movement, movement.idle);
        else if (PlayerInput.Instance.isDodge) ExitState(movement, movement.dodge);
        else if (PlayerInput.Instance.isSkillE) ExitState(movement, movement.skillE);
        else if (PlayerInput.Instance.isJump)
        {
            movement.previousState = this;
            ExitState(movement, movement.jump);
        }
        // else if () ExitState(movement, movement.death); //�׾������� ����
        movement.currentMoveSpeed = movement.walkSeed;
        PlayerAnimationManager.Instance.Move(PlayerInput.Instance.moveInput.x, PlayerInput.Instance.moveInput.y);
    }
    void ExitState(PlayerMovementManager movement, AllBodyBaseState state) //��ȯ�Ϸ����ϴ� ���¸� �𸥴ٴ� �������� �Լ��� ȣ��
    {
        PlayerAnimationManager.Instance.animator.SetBool("IsWalking", false);
        movement.SwitchState(state);
    }
}
