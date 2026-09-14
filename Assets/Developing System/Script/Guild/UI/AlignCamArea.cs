using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AlignCamArea : MonoBehaviour, IPointerEnterHandler
{
    public UnityEvent OnPointerIn;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerIn?.Invoke();
    }
}
