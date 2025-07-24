//**********23.01.07
//**********Boss_WalkState : 보스 움직임 관련 상태 클래스
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_WalkState : Boss_BaseState
{
    public override void EnterState(BossManager boss)
    {
        boss.animator.SetBool("IsWalk", true);
    }

    public override void UpdateState(BossManager boss)
    {
        /*if(boss.dis <= boss.distance)
        {
            boss.isWalk = false;
            boss.animator.SetBool("IsWalk", boss.isWalk);
        }
        else if (boss.dis > boss.distance)
        {
            Debug.Log("cc");
            boss.isWalk = true;
            boss.animator.SetBool("IsWalk", boss.isWalk);
        }*/
        

    }
}
