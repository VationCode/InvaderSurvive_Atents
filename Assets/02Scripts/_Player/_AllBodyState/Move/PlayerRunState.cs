//**********22.12.26 : Idle 상태 뺌(애니메이터에서 달리기상태바로 전환이며 안에 Idle가 있음) - 허인호
//**********22.12.24 : 달리는 애니메이션 동작과 달리기 속도 제어 - 허인호
//**********PlayerRunState : 달리기 상태 클래스
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
        //else if () ExitState(movement, movement.death); //죽었을때는 아직
        //if (PlayerInput.Instance.moveInput.y < 0) movement.currentMoveSpped = movement.runBackSpeed; //뒤로가기 했을때
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

    void ExitState(PlayerMovementManager movement, AllBodyBaseState state) //전환하려고하는 상태를 모른다는 가정하의 함수로 호출
    {
        PlayerAnimationManager.Instance.animator.SetBool("IsRunning", false);
        movement.SwitchState(state);
    }
}
