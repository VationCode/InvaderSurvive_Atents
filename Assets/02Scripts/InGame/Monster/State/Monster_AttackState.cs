//**********23.01.12 : 플레이어 공격 - 허인호 
//**********Monster_AttackState : 공격 행동 상태 클래스
using UnityEngine;

public class Monster_AttackState : MonsterBaseState
{
    bool isAttack = false;
    float attackT = 0.25f;
    float attackCounter = 0f;
    int attackNum;
    public override void EnterState(Monster monster)
    {
        //monster.animator.SetBool("IsWalk", false);
        //monster.animator.SetBool("IsAttack", true);
        //monster.navAgent.enabled = false;
        //attackCounter = 0f;
        //공격패턴들 받아옴
    }

    public override void UpdateState(Monster monster)
    {
        //죽었을 때
        //대상에게 각 공격들 랜덤으로 진행
        //적이 범위에서 벗어났을 경우(완전히 벗어났을 경우)
        //적이 공격범위에서 벗어났을 경우
        //그외 공격

        if (monster.isDead)
        {
            monster.SwitchState(monster.dead);
        }
        /*else if (Vector3.Distance(monster.originPos, monster.gameObject.transform.position) >= monster.originPosDis)
        {
            //monster.SwitchState(monster.returnMove);
        }
        else if(Vector3.Distance(monster.gameObject.transform.position, monster.player.transform.position) > monster.stopDis)
        {
            monster.SwitchState(monster.discoverMove);
        }*/
        else
        {
            //monster.animator.SetBool("IsAttack",true);
            /*if (monster.monsterInfo. == EAttackType2.far)
            {
                attackT = 0.25f;
            }
            else
            {
                attackT = 2.0f;
            }
            
            if(isAttack)
            {
                monster.animator.SetTrigger("Attack");
                isAttack = false;
            }
            else if(!isAttack)
            {
                attackCounter += Time.deltaTime;
                if (attackCounter > attackT)
                {
                    isAttack = true;
                    attackCounter = 0f;
                }
            }*/
            monster.isBaseAttack = true;
            //monster.gameObject.transform.LookAt(monster.player.transform);
        }
    }
}
