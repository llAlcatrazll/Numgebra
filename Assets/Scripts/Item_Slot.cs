using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Slot : MonoBehaviour, IDropHandler
{
    [SerializeField] public int requiredItemID;
    [SerializeField] private Local_Observer gameObserver;

    [SerializeField] private bool hasMultiAnswer;

    // [SerializeField] private DragnDrop_GameObserver gameObserver;  
    public void OnDrop(PointerEventData eventData)
    {
        // Get the dragged item
        Draggable_Item dragItem = eventData.pointerDrag?.GetComponent<Draggable_Item>();
        // snap item to position
        dragItem.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        if (dragItem.itemSlotID == requiredItemID)
        {
            return;
        }
        else if (dragItem.itemID == requiredItemID)
        {
            // ITEM IS PLACED CORRECT
            Debug.Log("Item is placed Correctly");
            dragItem.isPlaced = true;
            dragItem.isCorrectlyPlaced = true;
            gameObserver.correctlyPlacedItems++;

            if (dragItem.itemSlotID == 99999)
            {
                gameObserver.itemsPlaced++;
            }
            dragItem.itemSlotID = requiredItemID;
            gameObserver.CheckallItemSlots();

        }
        else if (dragItem.itemID != requiredItemID)
        {
            // ITEM IS PLACE INCORRECTLY
            Debug.Log("Item is placed Wrong");
            if (dragItem.itemSlotID == 99999)
            {
                gameObserver.itemsPlaced++;

            }
            dragItem.isPlaced = true;

            dragItem.itemSlotID = requiredItemID;
            if (dragItem.isCorrectlyPlaced)
            {
                dragItem.isCorrectlyPlaced = false;
                gameObserver.correctlyPlacedItems--;
            }

            gameObserver.CheckallItemSlots();
        }
        else
        {
            Debug.Log("Item not placed on any item slot returning");
        }
    }
}

