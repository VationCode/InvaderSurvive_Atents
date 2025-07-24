//플레이어 체크가 안됨;;
//순찰 범위 들어왔을 시 체크
using UnityEngine;
using BehaviorTree;

public class CheckEnemyInFOVRange : Node
{

    private Transform _transform;
    private Animator animator;
    public CheckEnemyInFOVRange(Transform transfomr)
    {
        _transform = transfomr;
        animator = transfomr.GetComponent<Animator>();
    }

    public override ENodeState Evaluate()
    {
        object t = GetData("Player");
        if(t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position,GuardBT.fovRange ,1 << LayerMask.NameToLayer("Player"));
            
            if(colliders.Length > 0)
            {
                Debug.Log(colliders[0].name);
                parent.parent.SetData("Player", colliders[0].transform);
                animator.SetBool("IsWalk", true);
                
                eState = ENodeState.SUCCESS;
                return eState;
            }
            eState = ENodeState.FAILURE;
            return eState;
        }
        eState = ENodeState.SUCCESS;
        return eState; 
    }
}
