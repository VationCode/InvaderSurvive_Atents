using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ATTACKTYPE
{
    Melee,
    Far
}

public enum MONSTERTYPE
{
    Normal,
    Boss
}

[Serializable]
public struct MonsterInfo
{
    public int id;
    public string name;
    public int hp;
    public int damage;
    public float moveSpeed;
    public MONSTERTYPE type;
    public ATTACKTYPE attacktType;
    public List<MonsterSkillInfo> monsterSkillList;
    public int dropItemListNum;
}