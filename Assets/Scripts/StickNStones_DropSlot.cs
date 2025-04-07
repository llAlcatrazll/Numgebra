using UnityEngine;
using UnityEngine.EventSystems;

public class StickNStones_DropSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        StickNStones_DraggableItem draggable = eventData.pointerDrag.GetComponent<StickNStones_DraggableItem>();
        if (draggable != null)
        {
            draggable.parentAfterDrag = transform;
            draggable.transform.SetParent(transform);
        }
    }
}