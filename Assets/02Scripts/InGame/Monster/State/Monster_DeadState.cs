//**********23.01.12 : 원 자리와의 일정 거리가 멀어졌을 경우 제자리로 복귀 - 허인호 
//**********Monster_ReturnOriginPosMoveState : 제자리 돌아가는 상태 클래스
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_DeadState : MonsterBaseState
{
    float deadT = 4.0f;
    float deadCounter = 0f;
    public override void EnterState(Monster monster)
    {
        monster.animator.SetBool("IsWalk", false);
        monster.animator.SetBool("IsAttack", false);
        //monster.animator.SetBool("IsDead", true);
        monster.animator.SetTrigger("Dead");
    }

    // Update is called once per frame
    public override void UpdateState(Monster monster)
    {
        if (monster.monsterInfo.type == MONSTERTYPE.Normal) deadT = 4.0f;
        else if (monster.monsterInfo.type== MONSTERTYPE.Boss) deadT = 15.0f;

            deadCounter += Time.deltaTime;

            if (deadCounter >= deadT)
        {
            deadCounter = 0f;
            GameObject.Destroy(monster.gameObject);
        }
    }
}
