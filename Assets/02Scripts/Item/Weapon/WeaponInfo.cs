using System;
using UnityEngine.UI;

public enum EWeapon
{
    Gun,
    Sword,
    Tool
}

[Serializable]
public struct WeaponInfo
{
    public EWeapon eWeapon;
    public string weaponName;
    public Image iconImage;
}