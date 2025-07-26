//**********23.01.16 : csv���Ϸκ��� ������ �޾ƿ����ϴ� ������� ����
//**********23.01.12 : ���� �޴� ������ ��ƼŬ, hp�� ���� �״� ǥ�� �Ϸ�
//**********23.01.12 : �÷��̾� ���� �� ����(��Ǹ�) �ϼ�(���� ���� �־����(���� ������ �ٸ� �����ִ� ���͵� ���� ���ϰ� �ϱ� ����))
//**********23.01.11 : ���� Ŭ���� ó���� FSM���� ������ BT �������������� ���������� ������ ����� ���ظ� ���߱⿡ ����� ������
//**********23.01.09 : MonsterInfo Ȯ��, �÷��̾��� ���� �� �Ÿ��� ���� �ൿ ������
//**********Monster : ���� Ŭ���� : ���Ϳ� ���� ������ �ϴ��� �������� ���ϴ� Ŭ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[Serializable]
public class Normal : Monster, IDamageabel
{
    float posY;
    void Start()
    {
        Init();
    }

    void Update()
    {
        if (PlayerMovementManager.Instance.isDead)
        {
            //SwitchState(patrol);
            return;
        }
        currentState.UpdateState(this);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
        else if (Physics.Raycast(transform.position, Vector3.up, out hit, Mathf.Infinity, groundLayer))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }

    
    public override bool ApplyDamage(DamageMessage damageMessage)
    {
        if (!base.ApplyDamage(damageMessage)) return false;
        
        return true;
    }
}
