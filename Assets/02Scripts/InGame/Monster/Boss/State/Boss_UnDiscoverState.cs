using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_UnDiscoverState : Boss_BaseState
{
    float t = 0;
    public override void EnterState(BossManager boss)
    {
        
    }

    public override void UpdateState(BossManager boss)
    {
        if (boss.isDiscover)
        {
            boss.animator.SetBool("IsDiscover", true);
            t += Time.deltaTime;
            if (t >= 4f)
            {
                boss.SwitchState(boss.idle);
                t = 0f;
            }
        }
    }
}
