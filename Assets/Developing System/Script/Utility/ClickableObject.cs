using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ClickableObject : MonoBehaviour
{
    [SerializeField] UnityEvent onClick;

    Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform != transform)
                return;

            onClick?.Invoke();
        }
    }
}