using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class TargetSelectionCam : MonoBehaviour
{
    [SerializeField] List<CinemachineCamera> cameras;

    [SerializeField] int baseCameraIndex = 5;

    [SerializeField] int activePriority = 10;
    [SerializeField] int inactivePriority = 0;

    public void UpdateCamera(Character target)
    {
        if (target is not Monster monster)
            return;

        int monsterIndex =
            BattleManager.Instance.MonsterCharacters.IndexOf(monster);

        if (monsterIndex < 0 || monsterIndex >= cameras.Count)
            return;

        SetCameraPriority(monsterIndex);
    }

    public void ResetCamera()
    {
        SetCameraPriority(baseCameraIndex);
    }

    void SetCameraPriority(int activeIndex)
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].Priority =
                i == activeIndex
                    ? activePriority
                    : inactivePriority;
        }
    }
}