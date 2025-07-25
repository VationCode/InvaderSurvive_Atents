//**********22.12.27 : �̴� ���� �� FSM��� ���� �Ϸ�
//**********22.12.26 : ���� ���������� FSM��������� �ٸ�....������
//**********22.12.26 : ���� �ִϸ��̼� ����
//**********PlayerJumpState : ���� ���� Ŭ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : AllBodyBaseState
{
    float t;
    public override void EnterState(PlayerMovementManager movement)
    {
        PlayerAnimationManager.Instance.Move(0,0);
        PlayerAnimationManager.Instance.animator.SetTrigger("Jump");
        movement.currentVelocityY += movement.jumpForce;
        movement.currentJumpCount--;
    }

    public override void UpdateState(PlayerMovementManager movement)
    {
        if (movement.isDead)
        {
            movement.SwitchState(movement.death);
            return;
        }
        PlayerAnimationManager.Instance.Move(0, 0);
        if (movement.isJump && movement.IsGrounded()) //�� �� �ٽ� �ٴۿ� ����������. (IsGrounded= false(�ڻ���)�� if�� ���� ����� �Ϸ��� ���� ���������� ����)
        {
            movement.currentJumpCount = movement.jumpCount;
            movement.isJump = false;
            movement.SwitchState(movement.run);
            t = 0f;
        }
        else if (!movement.IsGrounded())
        {
            t += Time.deltaTime;
            if (movement.currentJumpCount > 0 && t >= movement.jumpT)
            {
                if (PlayerInput.Instance.isJump)
                {
                    movement.currentVelocityY += (movement.jumpForce2);
                    PlayerAnimationManager.Instance.animator.SetTrigger("Jump");
                    movement.currentJumpCount--;
                    t = 0f;
                }
            }
        }
    }
}
