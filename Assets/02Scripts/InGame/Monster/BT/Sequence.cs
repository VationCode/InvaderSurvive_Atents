using System.Collections.Generic;
namespace BehaviorTree
{
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }
        public override ENodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (Node node in children)
            {
                switch(node.Evaluate())
                {
                    case ENodeState.FAILURE:
                        eState = ENodeState.FAILURE;
                        return eState;
                    case ENodeState.SUCCESS:
                        continue;
                    case ENodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        eState = ENodeState.SUCCESS;
                        return eState;
                }
            }
            eState = anyChildIsRunning ? ENodeState.RUNNING : ENodeState.SUCCESS;
            return eState;
        }
    }
}
