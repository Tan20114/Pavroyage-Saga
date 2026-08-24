using UnityEngine;

public class BehaviorSelectorNode : BehaviorCompositeNode
{
    public BehaviorSelectorNode(Blackboard blackboard) : base(blackboard)
    {
    }

    public override NodeStatus Tick()
    {
        foreach (BehaviorNode child in children)
        {
            NodeStatus status = child.Tick();

            if (status == NodeStatus.Success)
                return NodeStatus.Success;

            if (status == NodeStatus.Running)
                return NodeStatus.Running;
        }

        return NodeStatus.Failure;
    }
}
