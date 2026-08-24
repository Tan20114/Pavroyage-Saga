using System.Collections.Generic;

public class BehaviorCompositeNode : BehaviorNode
{
    protected readonly List<BehaviorNode> children = new();

    public BehaviorCompositeNode(Blackboard blackboard) : base(blackboard)
    {

    }

    public void AddChild(BehaviorNode child)
    {
        children.Add(child);
    }

    public override NodeStatus Tick()
    {
        throw new System.NotImplementedException();
    }
}
