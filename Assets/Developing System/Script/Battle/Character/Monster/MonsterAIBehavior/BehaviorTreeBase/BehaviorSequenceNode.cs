using UnityEngine;

public class BehaviorSequenceNode : BehaviorCompositeNode
{
    public BehaviorSequenceNode(Blackboard blackboard) : base(blackboard)
    {
    }

    public override NodeStatus Tick()
    {
        foreach (BehaviorNode child in children)
        {
            NodeStatus status = child.Tick();

            if (status == NodeStatus.Failure)
                return NodeStatus.Failure;

            if (status == NodeStatus.Running)
                return NodeStatus.Running;
        }

        return NodeStatus.Success;
    }
}
