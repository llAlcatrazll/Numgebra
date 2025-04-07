using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NumberPicker : MonoBehaviour
{
    public int currentValue = 0;
    [SerializeField] private TMP_Text FinalValue;
    public void Increment()
    {
        currentValue++;
        UpdateTextTMP();
        Debug.Log("Current Value: " + currentValue);
    }

    public void Decrement()
    {
        currentValue--;
        UpdateTextTMP();
        Debug.Log("Current Value: " + currentValue);
    }

    public void UpdateTextTMP()
    {
        if (FinalValue != null)
        {
            FinalValue.text = currentValue.ToString();
            Debug.Log($"Text is being updated to true VALUE - currentValue = {currentValue}");
        }
        else
        {
            Debug.Log("value not assigned");
        }
    }
}
