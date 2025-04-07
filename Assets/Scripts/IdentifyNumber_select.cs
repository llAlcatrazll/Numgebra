using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IdentifyNumber_select : MonoBehaviour
{
    [SerializeField] private Local_Observer gameObserver;

    public int ValuetoCheck = 0; // No static, so each instance is independent.

    [SerializeField] public int valueID;
    [SerializeField] public int buttonID;
    [SerializeField] private TMP_Text FinalValue;


    public void Start()
    {
        if (FinalValue != null)
        {
            // Ensure text is not empty and parse its value
            if (string.IsNullOrEmpty(FinalValue.text) || !int.TryParse(FinalValue.text, out ValuetoCheck))
            {
                ValuetoCheck = 0; // Default to 0 if parsing fails
            }
        }
        UpdateHopCountText(); // Update UI immediately
    }

    public void IncrementVal()
    {
        if (buttonID == valueID)
        {
            ValuetoCheck++;
            Debug.Log($"Incremented: {ValuetoCheck}");
            UpdateHopCountText();
        }
        else
        {
            Debug.Log("No matching ID");
        }
    }

    public void DecrementVal()
    {
        if (buttonID == valueID)
        {
            ValuetoCheck--;
            Debug.Log($"Decremented: {ValuetoCheck}");
            UpdateHopCountText();
        }
        else
        {
            Debug.Log("No matching ID");
        }
    }

    private void UpdateHopCountText()
    {
        if (FinalValue != null)
        {
            FinalValue.text = ValuetoCheck.ToString();
        }
    }
}
