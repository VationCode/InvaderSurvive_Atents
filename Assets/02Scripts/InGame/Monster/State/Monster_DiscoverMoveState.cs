//**********23.01.11 : �� �߰� �� ���� ���� ����(������ �̵��� ���� ��Ȳ��) - ����ȣ 
//**********Monster_DiscoverMoveState : �� �߰� �� ���� Ŭ����
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
        // ������ �̵�
        else
        {
            /*monster.navAgent.SetDestination(monster.player.transform.position);
            monster.navAgent.stoppingDistance = monster.stopDis;
            if (Vector3.Distance(monster.transform.position, monster.player.transform.position) <= monster.stopDis) //���ݹ����� ������ ��
            {
                //monster.SwitchState(monster.attack);
            }*/
        }
    }
}
