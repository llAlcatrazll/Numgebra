using UnityEngine;
using UnityEngine.EventSystems;

public class Alternate_Item_Slot : MonoBehaviour, IDropHandler
{
    [SerializeField] public int[] allowedItemIDs = new int[3]; // Allowed item IDs for this slot
    [SerializeField] public int[] requiredPairs = new int[3];  // Corresponding valid pairs in the other slot

    [SerializeField] private Local_Observer gameObserver;
    [SerializeField] private Alternate_Item_Slot checkOtherSlot; // Reference to the other slot

    public void OnDrop(PointerEventData eventData)
    {
        Draggable_Item dragItem = eventData.pointerDrag?.GetComponent<Draggable_Item>();
        if (dragItem == null) return;

        // Snap item to position
        dragItem.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

        // Find if the dropped item is in allowedItemIDs list
        int itemIndex = System.Array.IndexOf(allowedItemIDs, dragItem.itemID);
        bool isValidForThisSlot = itemIndex != -1;

        // Find the required match for the other slot
        int requiredMatchForOther = (itemIndex != -1) ? requiredPairs[itemIndex] : 99999;

        // Check if the other slot has the correct item
        bool isValidForOtherSlot = checkOtherSlot != null && checkOtherSlot.HasValidItem(requiredMatchForOther);

        if (isValidForThisSlot && (checkOtherSlot == null || isValidForOtherSlot))
        {
            // Correct placement
            Debug.Log($"Item {dragItem.itemID} placed correctly in slot {gameObject.name}");
            dragItem.isPlaced = true;

            if (!dragItem.isCorrectlyPlaced)
            {
                dragItem.isCorrectlyPlaced = true;
                gameObserver.correctlyPlacedItems++;
            }

            if (dragItem.itemSlotID == 99999)
            {
                gameObserver.itemsPlaced++;
            }
            dragItem.itemSlotID = dragItem.itemID;
        }
        else
        {
            // Incorrect placement
            Debug.Log($"Item {dragItem.itemID} placed incorrectly in slot {gameObject.name}");
            dragItem.isPlaced = true;

            if (dragItem.isCorrectlyPlaced)
            {
                dragItem.isCorrectlyPlaced = false;
                gameObserver.correctlyPlacedItems--;
            }

            if (dragItem.itemSlotID == 99999)
            {
                gameObserver.itemsPlaced++;
            }
            dragItem.itemSlotID = dragItem.itemID;
        }

        gameObserver.CheckallItemSlots();
    }

    public bool HasValidItem(int requiredItem)
    {
        // Checks if the slot currently has a valid item
        Draggable_Item currentItem = GetComponentInChildren<Draggable_Item>();
        return currentItem != null && currentItem.itemID == requiredItem;
    }
}
