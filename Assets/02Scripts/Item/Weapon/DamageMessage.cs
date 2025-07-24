//**********23.01.04 : 총 발사 시 혹은 다른 동작 시 Gun의 정보담고 있는 구조체 - 허인호 
//**********DamageMessage : Gun정보 전달 구조체
using UnityEngine;
public struct DamageMessage
{
    public GameObject damager;
    public int damage;

    public Vector3 hitPoint;
    public Vector3 hitNormal;
}
