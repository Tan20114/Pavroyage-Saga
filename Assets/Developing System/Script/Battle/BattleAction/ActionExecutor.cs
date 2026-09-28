using System.Collections;
using UnityEngine;

public class ActionExecutor
{
    const float ActionDelay = 2;

    readonly Character owner;

    public ActionExecutor(Character owner)
    {
        this.owner = owner;
    }

    public void Execute(IBattleAction action)
    {
        if (!action.CanExecute()) return;

        owner.StartCoroutine(ExecuteCoroutine(action));
    }

    IEnumerator ExecuteCoroutine(IBattleAction action)
    {
        if (owner is not Monster && 
            action.AnimationState == "Attack" || action.AnimationState == "Skill")
            yield return new WaitForSeconds(ActionDelay);

        if (owner.animator && !string.IsNullOrEmpty(action.AnimationState))
        {
            owner.animator.SetTrigger(action.AnimationState);

            yield return new WaitUntil(() => owner.animator.GetCurrentAnimatorStateInfo(0).IsName(action.AnimationState));

            yield return new WaitForSeconds(owner.animator.GetCurrentAnimatorStateInfo(0).length);
        }

        action.Execute();

        if (action.EndsTurn)
            BattleManager.Instance.NextTurn();
    }
}