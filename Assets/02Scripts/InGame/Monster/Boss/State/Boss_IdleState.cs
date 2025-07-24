using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_IdleState : Boss_BaseState
{
    public override void EnterState(BossManager boss)
    {
        
    }

    public override void UpdateState(BossManager boss)
    {
        boss.SwitchState(boss.laser);
        /*if(boss.isWalk)
        {
            boss.SwitchState(boss.walk);
        }*/
       
    }
}
