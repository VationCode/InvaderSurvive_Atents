//**********23.01.08 : ������ ���� �������� �Ѱ����� ó���� �� �ְ� ������ - ����ȣ 
//**********MonsterInfo : ���� ���� ���� ����ü
using System;
using UnityEngine;

public enum EMonsterType
{
    normal,
    Boss
}

[Serializable]
public enum EAttackType2
{
    melee, //�ٰŸ�
    far,   //���Ÿ�
    mix    //ȥ��
}
[Serializable]
public struct DropItem
{
    public Item item;
    public int itemCount;
}
[Serializable]
public struct Attack
{
    public float attackDistance; //�׺�޽� ���ߴ°Ÿ��ε� Ȱ��
    public int damage;
}
[Serializable]
public struct MonsterInfos
{
    public EMonsterType eMonsterType;
    public EAttackType2 eAttackType;
    public Attack[] attacks;
    public string name;
    public int hp;
    public int currentHP;
    public DropItem[] dropItem; //���� ������ �Ŵ����� ���� ����
}
