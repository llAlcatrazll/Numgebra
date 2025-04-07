using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableItem : MonoBehaviour
{
    [SerializeField] private CLickableItemObserver gameObserver;
    [SerializeField] private enablePaintCan PaintCan;
    [SerializeField] private int itemID;
    [SerializeField] private GameObject PaintMark;

    public bool isSlashed;

    public void EnableCheckMark()
    {
        if (!isSlashed && PaintCan.PaintCan)
        {
            isSlashed = true;
            PaintMark?.SetActive(true);
            Debug.Log("Item is Checked");

            UpdateCounter(1); // Increment counter
        }
        else
        {
            if (PaintMark != null && PaintMark.activeSelf)
            {
                UpdateCounter(-1); // Decrement counter
            }

            isSlashed = false;
            PaintMark?.SetActive(false);
            Debug.Log("Item is UnChecked");
        }
    }

    private void UpdateCounter(int change)
    {
        switch (itemID)
        {
            case -2:
                gameObserver.Neg2ItemsToggled += change;
                break;
            case -6:
                gameObserver.Neg6ItemsToggled += change;
                break;
            case -4:
                gameObserver.Neg4ItemsToggled += change;
                break;
            case 5:
                gameObserver.FiveItemsToggled += change;
                break;
            case 3:
                gameObserver.ThreeItemsToggled += change;
                break;
            case -7:
                gameObserver.Neg7ItemsToggled += change;
                break;
        }
    }
}
