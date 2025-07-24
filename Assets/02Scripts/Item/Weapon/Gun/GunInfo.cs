//**********23.01.04 : Gun의 정보담고 있는 구조체 - 허인호 
//**********GunInfo : Gun정보 틀 구조체
using UnityEngine;
using System;

public enum EGunType
{
    Rifle,
    ShotGun,
    Sniper,
    Laser
}

[Serializable]
public struct GunInfo
{
    public WeaponInfo weaponInfo;
    public EGunType eGunType;
    public string name;
    public string shootSoundName;
    public string reloadSoundName;
    public string particleName;
    public string bulletMaterrialName;
    public int damage;
    public float fireT; //발사 간격
    [Tooltip("총 탄약 수")]public int ammoRemain;
    [Tooltip("탄창에 있는 탄약 수")] public int magAmmo;
    [Tooltip("탄창에 넣을 수 있는 탄약 수")] public int magCapacity;
}
