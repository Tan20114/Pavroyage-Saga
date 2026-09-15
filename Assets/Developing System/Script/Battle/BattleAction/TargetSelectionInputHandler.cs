using System.Collections.Generic;
using UnityEngine;

public class TargetSelectionInputHandler : MonoBehaviour
{
    TargetSelection targetSelection;

    [SerializeField] TargetSelectionCam targetSelectionCam;

    public delegate void OnTargetConfirmed(Character character);
    public event OnTargetConfirmed onTargetConfirmed;

    public void Initialize(
        IReadOnlyList<Player> players,
        IReadOnlyList<Monster> monsters)
    {
        targetSelection = new TargetSelection(players, monsters);
    }

    public void OnAttack()
    {
        UpdateCamera();
    }

    public void OnNext()
    {
        targetSelection.Next();

        UpdateCamera();
    }

    public void OnPrevious()
    {
        targetSelection.Previous();

        UpdateCamera();
    }

    public void OnConfirm()
    {
        Character target = targetSelection.GetTarget();

        if (target == null)
            return;

        onTargetConfirmed?.Invoke(target);

        targetSelectionCam.ResetCamera();
    }

    void UpdateCamera()
    {
        Character target = targetSelection.GetTarget();

        if (target == null)
            return;

        targetSelectionCam.UpdateCamera(target);
    }
}