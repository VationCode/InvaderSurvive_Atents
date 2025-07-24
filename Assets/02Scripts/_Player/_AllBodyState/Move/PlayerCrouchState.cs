//**********22.12.24 : 앉은 애니메이션 동작과 걷는 속도 제어 - 허인호
//**********PlayerCrouchState : 앉기 상태 클래스
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
       // else if () ExitState(movement, movement.death); //죽었을때는 아직
        movement.currentMoveSpeed = movement.crouchSeed;
        PlayerAnimationManager.Instance.Move(PlayerInput.Instance.moveInput.x, PlayerInput.Instance.moveInput.y);
    }

    void ExitState(PlayerMovementManager movement, AllBodyBaseState state) //전환하려고하는 상태를 모른다는 가정하의 함수로 호출
    {
        PlayerAnimationManager.Instance.animator.SetBool("IsCrouching", false);

        movement.SwitchState(state);
    }
}
