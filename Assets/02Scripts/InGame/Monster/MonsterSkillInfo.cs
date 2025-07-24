using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EAttackTypes
{
    Base, //�⺻����
    Strong, //������
    Rush, //����
    Jump, //����
    AOE, //������
    //Laser //������
}

[Serializable]
public struct MonsterSkillInfo
{
    public string monsterName;
    public string skillName;
    public int attackStartDis;
    public int damage;
    public EAttackTypes eAttackType;
    public int angle; //���鿡�� ���ݵǴ� ��������
    public int range; //���� ���������� ��ä�� ����
    public int rotateCount; //������ angle�� rotateCount��ŭ ȸ��
}
