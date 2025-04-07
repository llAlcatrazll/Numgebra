using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_Item_Observer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Local_Observer gameObserver;

    [SerializeField] public int valueID;

    [SerializeField] public int CorrectValue;
    void Start()
    {

    }
    public void CheckGameCompete()
    {
        Debug.Log("Check game Compelted");
        if (valueID == CorrectValue)
        {
            gameObserver.correctlyPlacedItems++;
            gameObserver.itemsPlaced++;
            gameObserver.CheckallItemSlots();
        }
        else
        {
            if (gameObserver.replaytimes >= 2)
            {
                gameObserver.tipScreen.SetActive(true);
            }
            else
            {
                Debug.Log("Wrong Anser");
                gameObserver.incorrectInput.SetActive(true);
                gameObserver.replaytimes++;
            }
        }
    }

    void Update()
    {

    }
}
