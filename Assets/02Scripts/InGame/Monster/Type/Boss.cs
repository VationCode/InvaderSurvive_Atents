//**********23.01.16 : csv파일로부터 데이터 받아오게하는 방식으로 변경 - 허인호
//**********23.01.12 : 몬스터 받는 데미지 파티클, hp에 따른 죽는 표현 완료- 허인호
//**********23.01.12 : 플레이어 추적 및 공격(모션만) 완성(공격 패턴 넣어야함(차후 보스나 다른 패턴있는 몬스터들 관리 편하게 하기 위해)) - 허인호
//**********23.01.11 : 몬스터 클래스 처리를 FSM으로 변경중 BT 디자인패턴으로 적용했으나 내용이 어려워 이해를 못했기에 사용은 다음에 - 허인호 
//**********23.01.09 : MonsterInfo 확인, 플레이어의 공격 및 거리에 따른 행동 제작중 - 허인호 
//**********Boss : Boss 몬스터 클래스 : Boss 몬스터에 관한 정보를 일단은 수동으로 정하는 클래스
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[Serializable]
public class Boss : Monster, IDamageabel
{



    void Start()
    {
        Init();
        discoverDis = 20;
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public 
    override bool ApplyDamage(DamageMessage damageMessage)
    {
        if (!base.ApplyDamage(damageMessage)) return false;

        return true;
    }
}