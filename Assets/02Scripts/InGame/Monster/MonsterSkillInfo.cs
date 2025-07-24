using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EAttackTypes
{
    Base, //기본공격
    Strong, //강공격
    Rush, //돌진
    Jump, //점프
    AOE, //광범위
    //Laser //레이저
}

[Serializable]
public struct MonsterSkillInfo
{
    public string monsterName;
    public string skillName;
    public int attackStartDis;
    public int damage;
    public EAttackTypes eAttackType;
    public int angle; //정면에서 공격되는 범위각도
    public int range; //공격 각도에서의 부채꼴 범위
    public int rotateCount; //공격을 angle씩 rotateCount만큼 회전
}
