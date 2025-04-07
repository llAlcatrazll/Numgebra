using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IdentifyGameObserver : MonoBehaviour
{
    [SerializeField] public TMP_Text ChecValue1;
    [SerializeField] public TMP_Text ChecValue2;
    [SerializeField] public TMP_Text ChecValue3;
    [SerializeField] public TMP_Text ChecValue4;
    [SerializeField] public TMP_Text ChecValue5;
    [SerializeField] public TMP_Text ChecValue6;
    [SerializeField] public TMP_Text ChecValue7;

    [SerializeField] public NumberPicker Script1;
    [SerializeField] public NumberPicker Script2;
    [SerializeField] public NumberPicker Script3;
    [SerializeField] public NumberPicker Script4;
    [SerializeField] public NumberPicker Script5;
    [SerializeField] public NumberPicker Script6;
    [SerializeField] public NumberPicker Script7;

    [SerializeField] public int slotstoComplete;

    [SerializeField] private Local_Observer gameObserver;

    [SerializeField] public int AnswerVal1;
    [SerializeField] public int AnswerVal2;
    [SerializeField] public int AnswerVal3;
    [SerializeField] public int AnswerVal4;
    [SerializeField] public int AnswerVal5;
    [SerializeField] public int AnswerVal6;
    [SerializeField] public int AnswerVal7;

    [SerializeField] public GameObject Stage8Fog;
    public bool isStage8;
    private CanvasGroup treeCanvasGroup;

    [SerializeField] private float fadeDuration = 1.0f;
    public void CheckGameComplete()
    {
        int value1 = ConvertTextToInt(ChecValue1);
        int value2 = ConvertTextToInt(ChecValue2);
        int value3 = ConvertTextToInt(ChecValue3);
        int value4 = ConvertTextToInt(ChecValue4);
        int value5 = ConvertTextToInt(ChecValue5);
        int value6 = ConvertTextToInt(ChecValue6);
        int value7 = ConvertTextToInt(ChecValue7);

        Debug.Log($"Converted Values: {value1}, {value2}, {value3}, {value4}, {value5}, {value6}, {value7}");
        if (value1 == AnswerVal1 && value2 == AnswerVal2 && value3 == AnswerVal3 && slotstoComplete == 3)
        {
            gameObserver.itemsPlaced += 3;
            gameObserver.correctlyPlacedItems += 3;
            gameObserver.CheckallItemSlots();
            Debug.Log("Game Complete!");
            return;
        }
        if (value1 == AnswerVal1 && value2 == AnswerVal2 && value3 == AnswerVal3 && value4 == AnswerVal4 && slotstoComplete == 4)
        {
            if (isStage8)
            {
                Debug.Log("Trying to hide fog");
                // hide the game object (Stage8Fog) within 1 second
                StartCoroutine(FadeOut(Stage8Fog));
            }
            gameObserver.itemsPlaced += 4;
            gameObserver.correctlyPlacedItems += 4;
            gameObserver.CheckallItemSlots();
            Debug.Log("Game Complete!");
            return;
        }
        if (value1 == AnswerVal1 && value2 == AnswerVal2 && value3 == AnswerVal3 && value4 == AnswerVal4 &&
                    value5 == AnswerVal5 && value6 == AnswerVal6 && slotstoComplete == 6)
        {
            gameObserver.itemsPlaced += 6;
            gameObserver.correctlyPlacedItems += 6;
            gameObserver.CheckallItemSlots();
            Debug.Log("Game Complete!");
            return;
        }
        if (value1 == AnswerVal1 && value2 == AnswerVal2 && value3 == AnswerVal3 && value4 == AnswerVal4 &&
            value5 == AnswerVal5 && value6 == AnswerVal6 && value7 == AnswerVal7 && slotstoComplete == 7)
        {
            gameObserver.itemsPlaced += 7;
            gameObserver.correctlyPlacedItems += 7;
            gameObserver.CheckallItemSlots();
            Debug.Log("Game Complete!");
            return;
        }
        // if (value1 != AnswerVal1)
        // {
        //     Script1.currentValue = 0;
        //     Script1.UpdateTextTMP();
        //     Debug.Log("Answer 1 is Incorrect");
        // }
        // if (value2 != AnswerVal2)
        // {
        //     Script2.currentValue = 0;
        //     Script2.UpdateTextTMP();
        //     Debug.Log("Answer 2 is Incorrect");
        // }
        // Debug.Log($"Checking value3: {value3} against AnswerVal3: {AnswerVal3}");
        // Debug.Log($"Raw TMP Value for ChecValue3: {ChecValue3.text}");
        if (slotstoComplete >= 3)
        {
            if (value1 != AnswerVal1)
            {
                Script1.currentValue = 0;
                Script1.UpdateTextTMP();
                Debug.Log("Answer 1 is Incorrect");
            }
            if (value2 != AnswerVal2)
            {
                Script2.currentValue = 0;
                Script2.UpdateTextTMP();
                Debug.Log("Answer 2 is Incorrect");
            }
            if (value3 != AnswerVal3)
            {
                Script3.currentValue = 0;
                Script3.UpdateTextTMP();
                Debug.Log("Answer 3 is Incorrect");
            }
            if (slotstoComplete >= 4)
            {
                if (value4 != AnswerVal4)
                {
                    Script4.currentValue = 0;
                    Script4.UpdateTextTMP();
                    Debug.Log("Answer 4 is Incorrect");
                }
                if (slotstoComplete >= 6)
                {
                    if (value5 != AnswerVal5)
                    {
                        Script5.currentValue = 0;
                        Script5.UpdateTextTMP();
                        Debug.Log("Answer 5 is Incorrect");
                    }
                    if (value6 != AnswerVal6)
                    {
                        Script6.currentValue = 0;
                        Script6.UpdateTextTMP();
                        Debug.Log("Answer 6 is Incorrect");
                    }
                    if (slotstoComplete >= 7)
                    {
                        if (value7 != AnswerVal7)
                        {
                            Script7.currentValue = 0;
                            Script7.UpdateTextTMP();
                            Debug.Log("Answer 7 is Incorrect");
                        }
                    }
                }
            }
        }

        gameObserver.CheckallItemSlots();
        // Log incorrect values and reset them
        // List<string> incorrectValues = new List<string>();

        // void ResetIfIncorrect(ref int value, int correctValue, TMP_Text textElement, string name)
        // {
        //     if (value != correctValue)
        //     {
        //         incorrectValues.Add($"{name}: {value} (Expected: {correctValue})");
        //         value = 0;
        //         textElement.text = "0"; // Update UI
        //     }
        // }

        // ResetIfIncorrect(ref value1, AnswerVal1, ChecValue1, "Value1");
        // ResetIfIncorrect(ref value2, AnswerVal2, ChecValue2, "Value2");
        // ResetIfIncorrect(ref value3, AnswerVal3, ChecValue3, "Value3");
        // ResetIfIncorrect(ref value4, AnswerVal4, ChecValue4, "Value4");
        // ResetIfIncorrect(ref value5, AnswerVal5, ChecValue5, "Value5");
        // ResetIfIncorrect(ref value6, AnswerVal6, ChecValue6, "Value6");
        // ResetIfIncorrect(ref value7, AnswerVal7, ChecValue7, "Value7");

        // Debug.Log("Game Not Yet Complete. Incorrect Values: " + string.Join(", ", incorrectValues));
        // if (slotstoComplete == 3)
        // {

        gameObserver.replaytimes++;
        gameObserver.itemsPlaced = 0;
        gameObserver.correctlyPlacedItems = 0;

        if (gameObserver.replaytimes == 2)
        {
            gameObserver.ForceTipScreen();
            Debug.Log("Force tip screen");
        }
        else
        {
            gameObserver.ForceErroShow();

        }
    }

    private IEnumerator FadeOut(GameObject obj)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();

        // Ensure CanvasGroup exists
        if (canvasGroup == null)
        {
            canvasGroup = obj.AddComponent<CanvasGroup>();
        }

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
        obj.SetActive(false);
    }
    private int ConvertTextToInt(TMP_Text textElement)
    {
        if (textElement == null || string.IsNullOrWhiteSpace(textElement.text))
        {
            return 0; // Return default value if text element is null or empty
        }

        if (int.TryParse(textElement.text, out int value))
        {
            return value;
        }
        else
        {
            Debug.LogError($"Invalid integer format in {textElement?.name}");
            return 0; // Default value if parsing fails
        }
    }

}
