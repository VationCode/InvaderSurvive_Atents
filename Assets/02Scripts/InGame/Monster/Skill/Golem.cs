using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    Monster monster;
    GameObject player;
    float attackDis;
    BaseAttack baseAttack;
    void Start()
    {
        baseAttack = new BaseAttack();
        monster = GetComponent<Monster>();
        player = PlayerMovementManager.Instance.playerObj;
        attackDis = 4;

    }

    
    void Update()
    {
        baseAttack.BaseAttackInit(monster);
        baseAttack.Attack(monster);
    }
}
