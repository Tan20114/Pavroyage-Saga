using UnityEngine;

public enum NodeStatus
{
    Success,
    Failure,
    Running
}

public abstract class BehaviorNode
{
    protected Blackboard blackboard;

    protected BehaviorNode(Blackboard blackboard)
    {
        this.blackboard = blackboard;
    }

    public abstract NodeStatus Tick();
}