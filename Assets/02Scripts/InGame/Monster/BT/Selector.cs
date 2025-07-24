using System.Collections.Generic;

namespace BehaviorTree
{
    public class Selector : Node
    {
       public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        public override ENodeState Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case ENodeState.FAILURE:
                        continue;
                    case ENodeState.SUCCESS:
                        eState = ENodeState.SUCCESS;
                        return eState;
                    case ENodeState.RUNNING:
                        eState = ENodeState.RUNNING;
                        return eState;
                    default:
                        continue;
                }
            }
            eState = ENodeState.FAILURE;
            return eState;
        }
    }
}
