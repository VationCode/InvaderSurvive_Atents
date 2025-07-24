//**********23.01.03 : 클래스에서 구조체로 변경 - 허인호
//**********Item : 아이템정보 구조체
using UnityEngine;
using System;
//아이템 타입
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
    [Tooltip("아이템 타입")]
    public EItemType eItemType;
    [Tooltip("아이템 타입이 사용아이템이고 어떤 사용아이템인지 표시")]
    public EUseItemType eUseItemType;
    [Tooltip("아이템의 아이콘")]
    public Sprite sprite;
    public string itmeName; //아이템 여러개 복사할 경우 뒤에 숫자들 붙기에 따로 지정
}
