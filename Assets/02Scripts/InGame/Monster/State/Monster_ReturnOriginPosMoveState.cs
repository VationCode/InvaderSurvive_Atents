//**********23.01.12 : �� �ڸ����� ���� �Ÿ��� �־����� ��� ���ڸ��� ���� - ����ȣ 
//**********Monster_ReturnOriginPosMoveState : ���ڸ� ���ư��� ���� Ŭ����
using UnityEngine;

public class Monster_ReturnOriginPosMoveState : MonsterBaseStateT
{
    // Start is called before the first frame update
    public override void EnterState(MonsterT monster)
    {
        monster.animator.SetBool("IsAttack",false);
        monster.animator.SetBool("IsWalk",true);
        monster.animator.SetBool("IsDead", false);
        monster.gameObject.transform.LookAt(monster.originPos);
        monster.isApplyDamage = false;
        monster.nav.enabled = true;
    }

    public override void UpdateState(MonsterT monster)
    {
        if(monster.isDead)
        {
            //monster.SwitchState(monster.dead);
        }
        
        if(Vector3.Distance(monster.originPos, monster.gameObject.transform.position) <= 0.6f)
        {
            monster.gameObject.transform.rotation = monster.originRot;
            //monster.SwitchState(monster.patrol);
        }
        else
        {
            monster.nav.SetDestination(monster.originPos);
            monster.nav.stoppingDistance = 0.0f;
        }
    }
}
