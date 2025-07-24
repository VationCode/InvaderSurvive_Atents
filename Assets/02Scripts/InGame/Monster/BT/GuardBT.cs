using System.Collections.Generic;
using BehaviorTree;

public class GuardBT : Tree
{
    public UnityEngine.Transform[] waypoints; //유니엔진.트리도 있어서 using 지워주고 이렇게 안그럼 모호한 에러 뜸
    public static float speed = 5f;
    public static float fovRange = 10f;
    public static float attackRange = 4f;
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform),
                new TaskAttack(transform),
            }),
            new Sequence(new List<Node>
            {
            new CheckEnemyInFOVRange(transform),
            new TaskGoToTarget(transform),
            }),
            new TaskPatrol(transform, waypoints),
        });
        return root;
    }
}
