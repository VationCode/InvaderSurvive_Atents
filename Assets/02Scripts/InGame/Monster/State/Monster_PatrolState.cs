//**********23.01.11 : ������ ��������Ʈ �̵��� ���� ���º�ȭ üũ - ����ȣ 
//**********Monster_WayPointMoveState : ��������Ʈ �̵� �� ���� Ŭ����
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
            Debug.Log(monster.gameObject.name + "�߰�");
            //monster.SwitchState(monster.discoverMove);
            monster.SwitchState(monster.attack);
        }
        else if(monster.isApplyDamage)
        {
            monster.SwitchState(monster.attack);
        }
        else if(!monster.Discover()) //�� �߰����� ������ ��
        {
            if (monster.wayPoints.Length < 2) return;
            if(isWait) //������ �� �� ��������Ʈ�� �������� �� ���� ��ٸ���
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
                if (Vector3.Distance(monster.gameObject.transform.position, wp) < 0.01f) //��������Ʈ �������� ��
                {
                    monster.gameObject.transform.position = wp;
                    waitCounter = 0f;
                    isWait = true;

                    currentWaypointIndex = (currentWaypointIndex + 1) % monster.wayPoints.Length;
                    monster.animator.SetBool("IsWalk", false);
                }
                else //�̵�
                {
                    monster.gameObject.transform.position = Vector3.MoveTowards(monster.gameObject.transform.position, wp, moveSpeed * Time.deltaTime);
                    monster.gameObject.transform.LookAt(wp);
                }
            }
        }


    }
}
