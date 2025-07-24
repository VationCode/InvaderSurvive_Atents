using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_IdleState : MonsterBaseState
{
    public override void EnterState(Monster monster)
    {
       
        
    }

    public override void UpdateState(Monster monster)
    {
        monster.SwitchState(monster.patrol);
    }
}
