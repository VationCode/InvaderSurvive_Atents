using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxeState2 : AllBodyBaseState
{
    float T;
    public override void EnterState(PlayerMovementManager movement)
    {
        PlayerAnimationManager.Instance.animator.SetBool("IsGun", false);
        PlayerAnimationManager.Instance.animator.SetBool("IsPickAxe", true);
    }

    public override void UpdateState(PlayerMovementManager movement)
    {
        T += Time.deltaTime;
        
            if(T < 3)
            {
                PlayerAnimationManager.Instance.animator.SetLayerWeight(1, 0);
            }

        //if (!PlayerInput.Instance.isAttack) movement.SwitchState(movement.run);
        PlayerAnimationManager.Instance.Move(PlayerInput.Instance.moveInput.x, PlayerInput.Instance.moveInput.y);
        if (PlayerInput.Instance.isAttack)
        {
            PlayerAnimationManager.Instance.animator.SetBool("IsGathering", true);
        }
        else
        {
            PlayerAnimationManager.Instance.animator.SetBool("IsGathering", false);
        }


    }
}
