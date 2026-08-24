using System;
using UnityEngine;

public class BehaviorActionNode : BehaviorLeafNode
{
    private readonly Func<NodeStatus> action;

    public BehaviorActionNode(Func<NodeStatus> action, Blackboard blackboard) : base(blackboard)
    {
        this.action = action;
    }

    public override NodeStatus Tick()
    {
        return action();
    }
}
