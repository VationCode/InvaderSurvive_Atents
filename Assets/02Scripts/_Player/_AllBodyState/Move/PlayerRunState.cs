//**********22.12.26 : Idle ���� ��(�ִϸ����Ϳ��� �޸�����¹ٷ� ��ȯ�̸� �ȿ� Idle�� ����) - ����ȣ
//**********22.12.24 : �޸��� �ִϸ��̼� ���۰� �޸��� �ӵ� ���� - ����ȣ
//**********PlayerRunState : �޸��� ���� Ŭ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : AllBodyBaseState
{
    public override void EnterState(PlayerMovementManager movement)
    {
        PlayerAnimationManager.Instance.animator.SetBool("IsRunning", true);
    }
    public override void UpdateState(PlayerMovementManager movement)
    {
        if (movement.isDead)
        {
            movement.SwitchState(movement.death);
            //ExitState(movement, movement.death);
            return;
        }
        PlayerAnimationManager.Instance.Move(PlayerInput.Instance.moveInput.x, PlayerInput.Instance.moveInput.y);
        movement.currentMoveSpeed = movement.runSeed;
        if (PlayerInput.Instance.isAttack) ExitState(movement, movement.walk);
        else if (PlayerInput.Instance.isCrouch) ExitState(movement, movement.crouch);
        //else if (movement.moveDirection.magnitude < 0.1f) ExitState(movement, movement.idle);
        else if (PlayerInput.Instance.isDodge) ExitState(movement, movement.dodge);
        else if (PlayerInput.Instance.isSkillE)
        {
            movement.isGunSkillE = true;
        }
        //else if () ExitState(movement, movement.death); //�׾������� ����
        //if (PlayerInput.Instance.moveInput.y < 0) movement.currentMoveSpped = movement.runBackSpeed; //�ڷΰ��� ������
        else if (PlayerInput.Instance.isJump)
        {
            movement.previousState = this;
            ExitState(movement, movement.jump);
        }
        else if(PlayerInput.Instance.isPickAxe)
        {
            ExitState(movement, movement.pickAxe2);
        }
    }

    void ExitState(PlayerMovementManager movement, AllBodyBaseState state) //��ȯ�Ϸ����ϴ� ���¸� �𸥴ٴ� �������� �Լ��� ȣ��
    {
        PlayerAnimationManager.Instance.animator.SetBool("IsRunning", false);
        movement.SwitchState(state);
    }
}
