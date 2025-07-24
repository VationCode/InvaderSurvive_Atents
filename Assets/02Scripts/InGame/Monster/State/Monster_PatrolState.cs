//**********23.01.11 : 지정된 웨이포인트 이동에 따른 상태변화 체크 - 허인호 
//**********Monster_WayPointMoveState : 웨이포인트 이동 시 상태 클래스
using UnityEngine;

public class Monster_PatrolState : MonsterBaseState
{
    bool isWait = false;
    float waitTime = 2f;
    float waitCounter = 0f;
    int currentWaypointIndex = 0;


    float moveSpeed;
    public override void EnterState(Monster monster)
    {
        moveSpeed = 2f;
        if (monster.wayPoints.Length >= 2) monster.animator.SetBool("IsWalk", true);
        else monster.animator.SetBool("IsWalk", false);
        monster.animator.SetBool("IsAttack", false);
        monster.animator.SetBool("IsDead", false);
        monster.isApplyDamage = false;
        monster.navAgent.enabled = false;
        monster.meleeDiscoverDis = 15;
        monster.farDiscoverDis = 20;

        if (monster.monsterInfo.attacktType == ATTACKTYPE.Melee)
        {
            monster.discoverDis = monster.meleeDiscoverDis;
        }
        else if (monster.monsterInfo.attacktType == ATTACKTYPE.Far)
        {
            monster.discoverDis = monster.farDiscoverDis;
        }
        if(monster.monsterInfo.type == MONSTERTYPE.Boss)
        {
            monster.discoverDis = monster.farDiscoverDis;
        }
    }

    public override void UpdateState(Monster monster)
    {
        if (monster.isDead)
        {
            monster.SwitchState(monster.dead);
        }
        //monster.navAgent.enabled = false;
        if (monster.Discover())
        {
            Debug.Log(monster.gameObject.name + "발견");
            //monster.SwitchState(monster.discoverMove);
            monster.SwitchState(monster.attack);
        }
        else if(monster.isApplyDamage)
        {
            monster.SwitchState(monster.attack);
        }
        else if(!monster.Discover()) //적 발견하지 못했을 때
        {
            if (monster.wayPoints.Length < 2) return;
            if(isWait) //멈췄을 때 즉 웨이포인트에 도달했을 때 조금 기다리기
            {
                waitCounter += Time.deltaTime;
                if (waitCounter >= waitTime)
                {
                    isWait = false;
                    monster.animator.SetBool("IsWalk", true);
                }
            }
            else 
            {
                Vector3 wp = monster.wayPoints[currentWaypointIndex];
                if (Vector3.Distance(monster.gameObject.transform.position, wp) < 0.01f) //웨이포인트 도착했을 시
                {
                    monster.gameObject.transform.position = wp;
                    waitCounter = 0f;
                    isWait = true;

                    currentWaypointIndex = (currentWaypointIndex + 1) % monster.wayPoints.Length;
                    monster.animator.SetBool("IsWalk", false);
                }
                else //이동
                {
                    monster.gameObject.transform.position = Vector3.MoveTowards(monster.gameObject.transform.position, wp, moveSpeed * Time.deltaTime);
                    monster.gameObject.transform.LookAt(wp);
                }
            }
        }


    }
}
