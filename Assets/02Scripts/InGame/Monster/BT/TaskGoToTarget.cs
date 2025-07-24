//플레이어에게 접근
using UnityEngine;
using BehaviorTree;


public class TaskGoToTarget : Node
{
    private Transform _transform;
    public TaskGoToTarget(Transform transform)
    {
        _transform = transform;
    }

    public override ENodeState Evaluate()
    {
        Transform player = (Transform)GetData("Player");
        if(Vector3.Distance(_transform.position, player.position) > 0.01f)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, player.position, GuardBT.speed * Time.deltaTime);
            _transform.LookAt(player.position);
        }
        eState = ENodeState.RUNNING;
        return eState;
    }
}
