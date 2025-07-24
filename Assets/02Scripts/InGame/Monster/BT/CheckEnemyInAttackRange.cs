//공격범위 들어왔을 시 공격
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
public class CheckEnemyInAttackRange : Node
{
    private Transform _transform;
    private Animator animator;
 
    public CheckEnemyInAttackRange(Transform transform)
    {
        _transform = transform;
        animator = transform.GetComponent<Animator>();
    }
    public override ENodeState Evaluate()
    {
        object t = GetData("Player");
        if(t == null)
        {
            eState = ENodeState.FAILURE;
            return eState;
        }

        Transform player = (Transform)t;
        if(Vector3.Distance(_transform.position, player.position) <= GuardBT.attackRange)
        {
            animator.SetBool("IsAttack", true);
            animator.SetBool("IsWalk", false);

            eState = ENodeState.SUCCESS;
            return eState;
        }
        eState = ENodeState.FAILURE;
        return eState;
    }
}
