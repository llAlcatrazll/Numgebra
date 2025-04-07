using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchard_Basket_Observer : MonoBehaviour
{
    [SerializeField] private Local_Observer gameobserver;
    [SerializeField] public int correctNumberofItems1;
    [SerializeField] public int correctNumberofItems2;
    [SerializeField] public int correctNumberofItems3;

    [SerializeField] private Special_Item_Slot slot1;
    [SerializeField] private Special_Item_Slot slot2;
    [SerializeField] private Special_Item_Slot slot3;

    public void CheckBasketComplete()
    {
        Debug.Log("Checking all Baskets...");
        Debug.Log("Slot 1: " + slot1.GetPlacedItemCount());
        Debug.Log("Slot 2: " + slot2.GetPlacedItemCount());
        Debug.Log("Slot 3: " + slot3.GetPlacedItemCount());

        if (slot1.GetPlacedItemCount() == correctNumberofItems1 &&
            slot2.GetPlacedItemCount() == correctNumberofItems2 &&
            slot3.GetPlacedItemCount() == correctNumberofItems3)
        {
            gameobserver.itemsPlaced += 3;
            gameobserver.correctlyPlacedItems += 3;
            gameobserver.CheckallItemSlots();
            Debug.Log("All baskets are correctly filled!");
        }
        else
        {
            gameobserver.replaytimes++;
            gameobserver.CheckallItemSlots();
            if (gameobserver.replaytimes == 2)
            {
                gameobserver.ForceTipScreen();
            }
            Debug.Log("Baskets are not yet complete.");
        }
    }
}
