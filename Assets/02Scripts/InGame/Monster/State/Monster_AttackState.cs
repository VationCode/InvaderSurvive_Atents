//**********23.01.12 : �÷��̾� ���� - ����ȣ 
//**********Monster_AttackState : ���� �ൿ ���� Ŭ����
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
        //�������ϵ� �޾ƿ�
    }

    public override void UpdateState(Monster monster)
    {
        //�׾��� ��
        //��󿡰� �� ���ݵ� �������� ����
        //���� �������� ����� ���(������ ����� ���)
        //���� ���ݹ������� ����� ���
        //�׿� ����

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
