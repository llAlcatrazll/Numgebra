using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoublebirdIncrementor : MonoBehaviour
{
    public int ValuetoCheck = 0;

    [SerializeField] private TMP_Text textDisplay;
    [SerializeField] public int CorrectValue;

    [SerializeField] private Local_Observer gameObserver;

    void Start()
    {
        UpdateValueText();
    }

    public void IncrementVal()
    {
        ValuetoCheck++;
        UpdateValueText();
    }

    public void DecrementVal()
    {
        ValuetoCheck--;
        UpdateValueText();
        Debug.Log("Decrement");
    }

    public void UpdateValueText()
    {
        if (textDisplay != null)
        {
            textDisplay.text = ValuetoCheck.ToString();
        }
        else
        {
            Debug.LogWarning("Text display is not assigned!");
        }
    }
    public void CheckValueCorrectness()
    {
        if (ValuetoCheck == CorrectValue)
        {
            gameObserver.correctlyPlacedItems++;
            gameObserver.itemsPlaced++;
            gameObserver.CheckallItemSlots();
        }
        else
        {
            gameObserver.replaytimes++;
            gameObserver.DraggableReset();
            gameObserver.CheckallItemSlots();
        }
        Debug.Log($"FinalVal: {ValuetoCheck}");
    }
}
