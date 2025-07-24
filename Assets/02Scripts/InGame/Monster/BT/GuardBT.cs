using System.Collections.Generic;
using BehaviorTree;

public class GuardBT : Tree
{
    public UnityEngine.Transform[] waypoints; //���Ͽ���.Ʈ���� �־ using �����ְ� �̷��� �ȱ׷� ��ȣ�� ���� ��
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
