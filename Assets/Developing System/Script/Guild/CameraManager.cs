using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    enum CamState
    {
        Left,
        Center,
        Right,
    }

    [SerializeField] List<CinemachineCamera> cam;
    CamState state = CamState.Center;

    public void AlignCam(int index)
    {
        switch (index)
        {
            case 0:
                state = state switch
                {
                    CamState.Left => CamState.Left,
                    CamState.Center => CamState.Left,
                    CamState.Right => CamState.Center,
                    _ => state
                };
                break;

            case 1:
                state = CamState.Center;
                break;

            case 2:
                state = state switch
                {
                    CamState.Left => CamState.Center,
                    CamState.Center => CamState.Right,
                    CamState.Right => CamState.Right,
                    _ => state
                };
                break;
        }

        foreach (CinemachineCamera c in cam)
        {
            c.Priority = 0;
        }

        cam[(int)state].Priority = 1;
    }

    public void GoToCam(int index)
    {
        foreach (CinemachineCamera c in cam)
        {
            c.Priority = 0;
        }

        cam[index].Priority = 1;
    }
}