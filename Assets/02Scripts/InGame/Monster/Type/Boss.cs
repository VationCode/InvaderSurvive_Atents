//**********23.01.16 : csv���Ϸκ��� ������ �޾ƿ����ϴ� ������� ���� - ����ȣ
//**********23.01.12 : ���� �޴� ������ ��ƼŬ, hp�� ���� �״� ǥ�� �Ϸ�- ����ȣ
//**********23.01.12 : �÷��̾� ���� �� ����(��Ǹ�) �ϼ�(���� ���� �־����(���� ������ �ٸ� �����ִ� ���͵� ���� ���ϰ� �ϱ� ����)) - ����ȣ
//**********23.01.11 : ���� Ŭ���� ó���� FSM���� ������ BT �������������� ���������� ������ ����� ���ظ� ���߱⿡ ����� ������ - ����ȣ 
//**********23.01.09 : MonsterInfo Ȯ��, �÷��̾��� ���� �� �Ÿ��� ���� �ൿ ������ - ����ȣ 
//**********Boss : Boss ���� Ŭ���� : Boss ���Ϳ� ���� ������ �ϴ��� �������� ���ϴ� Ŭ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[Serializable]
public class Boss : Monster, IDamageabel
{



    void Start()
    {
        Init();
        discoverDis = 20;
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public 
    override bool ApplyDamage(DamageMessage damageMessage)
    {
        if (!base.ApplyDamage(damageMessage)) return false;

        return true;
    }
}