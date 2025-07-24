//**********23.01.12 : �� �ڸ����� ���� �Ÿ��� �־����� ��� ���ڸ��� ���� - ����ȣ 
//**********Monster_ReturnOriginPosMoveState : ���ڸ� ���ư��� ���� Ŭ����
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
