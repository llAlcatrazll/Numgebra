using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickItem : MonoBehaviour
{

    [SerializeField] public int itemID;

    [SerializeField] private Local_Observer gameObserver;


    // HARD CODE STAGE ANSWER
    [SerializeField] public int GameStageAnswerVal;

    public void ShowID()
    {
        Debug.Log(itemID);
    }

    public void checkAnswer()
    {
        if (itemID == GameStageAnswerVal)
        {
            Debug.Log("Stage Complete");
            gameObserver.correctlyPlacedItems++;
            gameObserver.itemsPlaced++;
            gameObserver.CheckallItemSlots();
        }
        else
        {
            Debug.Log("Answer is wrong try again");
            gameObserver.replaytimes++;
            gameObserver.CheckallItemSlots();
        }
    }

}
