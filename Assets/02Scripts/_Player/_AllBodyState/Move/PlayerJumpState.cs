//**********22.12.27 : 이단 점프 및 FSM방식 대입 완료
//**********22.12.26 : 문제 기존점프와 FSM점프방식이 다름....생각중
//**********22.12.26 : 점프 애니메이션 동작
//**********PlayerJumpState : 점프 상태 클래스
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
        if (movement.isJump && movement.IsGrounded()) //뛴 후 다시 바닦에 떨어졌을때. (IsGrounded= false(뛴상태)로 if문 안의 내용들 하려니 여러 문제점들이 있음)
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
