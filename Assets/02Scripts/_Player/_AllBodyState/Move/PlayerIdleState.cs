//**********22.12.26 : Idle 상태 뺌(애니메이터에서 달리기상태바로 전환이며 안에 Idle가 있음)
//**********22.12.24 : 대기 애니메이션 동작
//**********PlayerRunState : 달리기 상태 클래스
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
        if (movement.moveDirection.magnitude > 0.1f) //0.1f보다 크면
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
