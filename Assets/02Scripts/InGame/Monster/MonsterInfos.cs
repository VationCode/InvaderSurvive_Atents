//**********23.01.08 : 몬스터의 여러 정보들을 한곳에서 처리할 수 있게 제작중 - 허인호 
//**********MonsterInfo : 몬스터 정보 전달 구조체
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
    melee, //근거리
    far,   //원거리
    mix    //혼합
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
    public float attackDistance; //네비메쉬 멈추는거리로도 활용
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
    public DropItem[] dropItem; //차후 아이템 매니저를 통해 개선
}
