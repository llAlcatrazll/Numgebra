using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MultiscrollLockObserver : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI Val1;
    [SerializeField] public TextMeshProUGUI Val2;
    [SerializeField] public TextMeshProUGUI Val3;
    [SerializeField] public TextMeshProUGUI Val4;
    [SerializeField] public TextMeshProUGUI Val5;
    [SerializeField] public TextMeshProUGUI Val6;
    [SerializeField] public TextMeshProUGUI Val7;
    [SerializeField] public TextMeshProUGUI Val8;
    [SerializeField] public TextMeshProUGUI Val9;

    [SerializeField] private Local_Observer gameObserver;
    [SerializeField] private GameObject entirePodlock;

    private CanvasGroup podlockCanvasGroup;

    public int valfirstColumn = 1;  // Tracks first column selection
    public int valsecondColumn = 1; // Tracks second column selection
    public int valthirdColumn = 1;  // Tracks third column selection

    public int correctAnswerCol1 = 3;
    public int correctAnswerCol2 = 5;
    public int correctAnswerCol3 = 6;

    private void Start()
    {
        podlockCanvasGroup = entirePodlock.GetComponent<CanvasGroup>();
        if (podlockCanvasGroup == null)
        {
            podlockCanvasGroup = entirePodlock.AddComponent<CanvasGroup>();
        }
        updateTextDisplay();
        CheckisLockCanUnlock();
    }

    public void CheckisLockCanUnlock()
    {
        if (gameObserver.correctlyPlacedItems >= 3)
        {
            podlockCanvasGroup.interactable = true;
            podlockCanvasGroup.blocksRaycasts = true;
            podlockCanvasGroup.alpha = 1f; // Fully visible
            gameObserver.PadlockInstructions.SetActive(true);
        }
        else
        {
            podlockCanvasGroup.interactable = false;
            podlockCanvasGroup.blocksRaycasts = false;
            podlockCanvasGroup.alpha = 0.5f; // Darkened
        }
    }

    // private void Update()
    // {
    // private void Update()
    // {
    //     if (gameObserver.correctlyPlacedItems >= 3)
    //     {
    //         podlockCanvasGroup.interactable = true;
    //         podlockCanvasGroup.blocksRaycasts = true;
    //         podlockCanvasGroup.alpha = 1f; // Fully visible
    //         gameObserver.PadlockInstructions.SetActive(true);
    //     }
    //     else
    //     {
    //         podlockCanvasGroup.interactable = false;
    //         podlockCanvasGroup.blocksRaycasts = false;
    //         podlockCanvasGroup.alpha = 0.5f; // Darkened
    //     }
    // }
    // }

    public void checkLockCompletion()
    {
        if (valfirstColumn == correctAnswerCol1 &&
            valsecondColumn == correctAnswerCol2 &&
            valthirdColumn == correctAnswerCol3)
        {

            Debug.Log("Unlocked!");
            gameObserver.itemsPlaced++;
            gameObserver.correctlyPlacedItems++;
            gameObserver.CheckallItemSlots();
        }
        else
        {
            gameObserver.incorrectInput.SetActive(true);
            Debug.Log("Incorrect Combination.");
        }
    }

    public void incrementVal(int column)
    {
        switch (column)
        {
            case 1:
                valfirstColumn++;
                break;
            case 2:
                valsecondColumn++;
                break;
            case 3:
                valthirdColumn++;
                break;
        }
        updateTextDisplay();
    }

    public void decrementVal(int column)
    {
        switch (column)
        {
            case 1:
                valfirstColumn--;
                break;
            case 2:
                valsecondColumn--;
                break;
            case 3:
                valthirdColumn--;
                break;
        }
        updateTextDisplay();
    }

    public void updateTextDisplay()
    {
        // Updates first column values
        Val1.text = valfirstColumn.ToString();
        Val4.text = (valfirstColumn - 1).ToString();
        Val7.text = (valfirstColumn + 1).ToString();

        // Updates second column values
        Val2.text = valsecondColumn.ToString();
        Val5.text = (valsecondColumn - 1).ToString();
        Val8.text = (valsecondColumn + 1).ToString();

        // Updates third column values
        Val3.text = valthirdColumn.ToString();
        Val6.text = (valthirdColumn - 1).ToString();
        Val9.text = (valthirdColumn + 1).ToString();
    }
}
