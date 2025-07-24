//**********23.01.07
//**********Boss_Attack3State : 보스 공격3 관련 상태 클래스
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_LaserState : Boss_BaseState
{
    float t = 0f;
    public override void EnterState(BossManager boss)
    {
        //boss.laserAttack.CreateWaringLine(boss.transform);
    }

    public override void UpdateState(BossManager boss)
    {
        /*boss.gameObject.transform.LookAt(PlayerMovementManager.Instance.playerObj.transform);
        boss.laserAttack.Laser();
        t += Time.deltaTime;
        if(t >= 3.0f)
        {
            t = 0f;
            boss.animator.SetTrigger("LaserAttack");
            boss.SwitchState(boss.idle);
        }*/
    }
}
