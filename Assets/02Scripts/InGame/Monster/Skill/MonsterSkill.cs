using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkill<T> where T : class
{
    public List<T> skillList;

    public MonsterSkill()
    {
        
    }

    void Start()
    {
        skillList = new List<T>();
        //BaseAttack = new BaseAttack();
    }
    public void GetSkill(MonsterInfo monsterInfo)
    {
        for (int i = 0; i < monsterInfo.monsterSkillList.Count; i++)
        {
            switch(monsterInfo.monsterSkillList[i].eAttackType)
            {
                case EAttackTypes.Base:
                    switch(monsterInfo.attacktType)
                    {
                        case ATTACKTYPE.Melee:
                            
                            break;
                        case ATTACKTYPE.Far:

                            break;
                    }
                    break;
                case EAttackTypes.Strong:

                    break;
                case EAttackTypes.Rush:

                    break;
                case EAttackTypes.Jump:

                    break;
                case EAttackTypes.AOE:

                    break;
               // case EAttackTypes.Laser:

                //    break;
            }
            
        }
    }
}
