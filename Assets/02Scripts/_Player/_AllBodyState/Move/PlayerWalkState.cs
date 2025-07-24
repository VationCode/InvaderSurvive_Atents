//**********22.12.24 : 걷는 애니메이션 동작과 걷기 속도 제어, 걷는 상태는 총을 발사할 때만 동작이됨 - 허인호
//**********PlayerWalkState : 걷기 상태 클래스
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
        // else if () ExitState(movement, movement.death); //죽었을때는 아직
        movement.currentMoveSpeed = movement.walkSeed;
        PlayerAnimationManager.Instance.Move(PlayerInput.Instance.moveInput.x, PlayerInput.Instance.moveInput.y);
    }
    void ExitState(PlayerMovementManager movement, AllBodyBaseState state) //전환하려고하는 상태를 모른다는 가정하의 함수로 호출
    {
        PlayerAnimationManager.Instance.animator.SetBool("IsWalking", false);
        movement.SwitchState(state);
    }
}
