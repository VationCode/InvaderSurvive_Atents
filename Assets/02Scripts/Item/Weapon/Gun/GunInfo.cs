//**********23.01.04 : Gun�� ������� �ִ� ����ü - ����ȣ 
//**********GunInfo : Gun���� Ʋ ����ü
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
    public float fireT; //�߻� ����
    [Tooltip("�� ź�� ��")]public int ammoRemain;
    [Tooltip("źâ�� �ִ� ź�� ��")] public int magAmmo;
    [Tooltip("źâ�� ���� �� �ִ� ź�� ��")] public int magCapacity;
}
