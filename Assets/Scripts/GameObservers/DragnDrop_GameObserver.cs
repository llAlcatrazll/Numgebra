using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragnDrop_GameObserver : MonoBehaviour
{
    public int replaytimes = 0;
    public int itemsPlaced = 0;
    public int correctlyPlacedItems = 0;
    [SerializeField] private int totalSlots; 
    [SerializeField] public GameObject completeScreen;
    [SerializeField] private GameObject tipScreen;
    [SerializeField] private Draggable_Item draggableItem;
    [SerializeField] private Stage_Record_Observer Stage_Complete;

    private Draggable_Item GetItemByID(int itemID)
    {
        return null;
    }

    public void CheckallItemSlots(){
        if(itemsPlaced == totalSlots)
        {
            Debug.Log("Check items");
            if(correctlyPlacedItems >= itemsPlaced)
            {
                // FINISH THE LEVEL
                completeScreen.SetActive(true);
                Stage_Complete.StageComplete();
            }
            if(correctlyPlacedItems < itemsPlaced && replaytimes == 1)
            // SHOW TIP SCREEN AFTER 2ND MISTAKE FOR EARLIER TIP
            {
                // SHOW TIP SCREEN
                tipScreen.SetActive(true);
                replaytimes++;
                DraggableReset();
            }
            else{
                // RESET TO REPLAY WITHOUT TIP SCREEN
                replaytimes++;
                DraggableReset();
            }
        }
    }

        public void DraggableReset()
    {
        Draggable_Item[] allDraggableItems = FindObjectsOfType<Draggable_Item>();
             itemsPlaced = 0;
        correctlyPlacedItems = 0;
        foreach (Draggable_Item draggableItem in allDraggableItems)
        {
            if (draggableItem.isPlaced && !draggableItem.isCorrectlyPlaced)
            {
                draggableItem.ResetPosition();
                draggableItem.itemSlotID = 99999;
            }
            if(draggableItem.isCorrectlyPlaced)
            {
                correctlyPlacedItems++;
                itemsPlaced++;
            }
        }
   

    }
}
