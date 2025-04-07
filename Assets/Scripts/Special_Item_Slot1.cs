using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Special_Item_Slot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Local_Observer gameObserver;
    [SerializeField] private Vector2 stackingOffset = Vector2.zero; // Default: No offset
    private List<appledraggableitem> placedItems = new List<appledraggableitem>();

    public void OnDrop(PointerEventData eventData)
    {
        appledraggableitem dragItem = eventData.pointerDrag?.GetComponent<appledraggableitem>();
        if (dragItem == null || placedItems.Contains(dragItem)) return; // Ignore invalid or duplicate items

        // Set the item as a child of this slot
        dragItem.transform.SetParent(transform, false);
        dragItem.transform.SetAsLastSibling(); // Ensure it's on top in hierarchy

        // Snap item position inside the slot
        RectTransform itemRect = dragItem.GetComponent<RectTransform>();
        itemRect.anchoredPosition = GetStackedPosition();

        // Register the item
        placedItems.Add(dragItem);
        dragItem.assignedSlot = this; // Mark item as placed in this slot

        if (gameObserver != null)
        {
            gameObserver.itemsPlaced++;
            gameObserver.CheckallItemSlots(); // Ensure observer method exists
        }
    }

    private Vector2 GetStackedPosition()
    {
        return stackingOffset * placedItems.Count; // Remove reliance on slot position
    }

    public void RemoveItem(appledraggableitem item)
    {
        if (placedItems.Remove(item) && gameObserver != null)
        {
            gameObserver.itemsPlaced--;
        }
    }

    public int GetPlacedItemCount() => placedItems.Count;
}
