using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttack : Node
{
    private Transform lastTarget;

    private float attackTime = 1f;
    private float attackCounter = 0f;
   public TaskAttack(Transform transform)
    {

    }
    public override ENodeState Evaluate()
    {
        Transform player = (Transform)GetData("Player");
        if(player != lastTarget)
        {

        }

        attackCounter += Time.deltaTime;
        if(attackCounter >= attackTime)
        {
            //bool �׾����� üũ
            //���� �׾��ٸ�
            //���� ���߰� �ȱ�
        }
        else
        {
            attackCounter = 0f;
        }
        eState = ENodeState.RUNNING;
        return eState;
    }
}
