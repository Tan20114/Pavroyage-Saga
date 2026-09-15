using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetSelectionInputHandler : MonoBehaviour
{
    TargetSelection targetSelection;

    public delegate void OnTargetConfirmed(Character character);
    public event OnTargetConfirmed onTargetConfirmed;

    public void Initialize(IReadOnlyList<Player> players,IReadOnlyList<Monster> monsters)
    {
        targetSelection = new TargetSelection(players, monsters);
    }

    public void OnNext()
    {
        targetSelection.Next();
    }

    public void OnPrevious()
    {
        targetSelection.Previous();
    }

    public void OnConfirm()
    {
        Character target = targetSelection.GetTarget();

        if (target == null)
            return;

        onTargetConfirmed?.Invoke(target);
    }
}