//**********23.01.03 : Ŭ�������� ����ü�� ���� - ����ȣ
//**********Item : ���������� ����ü
using UnityEngine;
using System;
//������ Ÿ��
public enum EItemType
{
    Equipment,
    Ingredient,
    UseItem,
    Weapon
}
public enum EUseItemType
{
    None,
    HealPotion,
    ManaPotion
}
[Serializable]
public struct ItemInfo
{
    [Tooltip("������ Ÿ��")]
    public EItemType eItemType;
    [Tooltip("������ Ÿ���� ���������̰� � ������������ ǥ��")]
    public EUseItemType eUseItemType;
    [Tooltip("�������� ������")]
    public Sprite sprite;
    public string itmeName; //������ ������ ������ ��� �ڿ� ���ڵ� �ٱ⿡ ���� ����
}
