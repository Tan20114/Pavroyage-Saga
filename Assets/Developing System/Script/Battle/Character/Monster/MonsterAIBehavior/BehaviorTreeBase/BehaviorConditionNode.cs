using System;
using UnityEngine;

public class BehaviorConditionNode : BehaviorLeafNode
{
    private readonly Func<bool> condition;

    public BehaviorConditionNode(Func<bool> condition, Blackboard blackboard) : base(blackboard)
    {
        this.condition = condition;
    }

    public override NodeStatus Tick()
    {
        return condition() ? NodeStatus.Success : NodeStatus.Failure;
    }
}