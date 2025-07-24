//**********23.01.11 : 적 발견 시 취할 상태 제작(적에게 이동할 때의 상황들) - 허인호 
//**********Monster_DiscoverMoveState : 적 발견 시 상태 클래스
using UnityEngine;

public class Monster_DiscoverMoveState : MonsterBaseState
{

    public override void EnterState(Monster monster)
    {
        monster.animator.SetBool("IsAttack", false);
        monster.animator.SetBool("IsWalk", true);
        monster.animator.SetBool("IsDead", false);
        monster.gameObject.transform.LookAt(monster.player.transform.position);
        monster.navAgent.enabled = true;
    }

    public override void UpdateState(Monster monster)
    {
        if (monster.isDead)
        {
            //monster.SwitchState(monster.dead);
        }
        Debug.Log("discoverupdate");
        if (Vector3.Distance(monster.originPos, monster.gameObject.transform.position) >= monster.originPosDis)
        {
            //monster.SwitchState(monster.returnMove);
        }
        // 적에게 이동
        else
        {
            /*monster.navAgent.SetDestination(monster.player.transform.position);
            monster.navAgent.stoppingDistance = monster.stopDis;
            if (Vector3.Distance(monster.transform.position, monster.player.transform.position) <= monster.stopDis) //공격범위에 들어왔을 때
            {
                //monster.SwitchState(monster.attack);
            }*/
        }
    }
}
