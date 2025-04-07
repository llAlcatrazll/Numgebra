using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TMP for text handling

public class CLickableItemObserver : MonoBehaviour
{
    [Header("General")]
    [SerializeField] public Local_Observer gameObserver;

    [Header("Val1")]
    public int IdentifyVal1 = 0;

    public int Neg4ItemsToggled;
    public int FiveItemsToggled;
    [SerializeField] private TMP_Text Val1Text;

    [SerializeField] private GameObject SelectNumber1;
    public void Val1Increment()
    {
        UpdateText1(++IdentifyVal1);
        CheckEverything();
    }
    public void Val1Decrement()
    {
        UpdateText1(--IdentifyVal1);
        CheckEverything();
    }
    private void UpdateText1(int value) => Val1Text?.SetText(value.ToString());
    public void CheckGroup1()
    {
        Debug.Log("Check group 1");
        if (Neg4ItemsToggled == 4 && FiveItemsToggled == 4)
        {
            Debug.Log("1st Group is correct");
            SelectNumber1.SetActive(true);
        }
        else
        {
            Debug.Log("Something is incorrect");
            if (gameObserver.replaytimes != 2)
            {
                gameObserver.ForceErroShow();
            }
        }
    }

    [Header("Val2")]
    public int IdentifyVal2 = 0;
    public int ThreeItemsToggled;
    public int Neg7ItemsToggled;
    [SerializeField] private TMP_Text Val2Text;

    [SerializeField] private GameObject SelectNumber2;

    public void Val2Increment()
    {
        UpdateText2(++IdentifyVal2);
        CheckEverything();
    }
    public void Val2Decrement()
    {
        UpdateText2(--IdentifyVal2);
        CheckEverything();
    }

    private void UpdateText2(int value) => Val2Text?.SetText(value.ToString());

    public void CheckGroup2()
    {
        Debug.Log("Check group 2");
        if (ThreeItemsToggled == 3 && Neg7ItemsToggled == 3)
        {
            Debug.Log("2nd Group is correct");
            SelectNumber2.SetActive(true);
        }
        else
        {
            Debug.Log("Something is incorrect");
            if (gameObserver.replaytimes != 2)
            {
                gameObserver.ForceErroShow();
            }
        }
    }

    [Header("Val3")]
    public int IdentifyVal3 = 0;
    public int Neg2ItemsToggled;
    public int Neg6ItemsToggled;

    [SerializeField] private TMP_Text Val3Text;

    [SerializeField] private GameObject SelectNumber3;
    public void Val3Increment()
    {
        UpdateText3(++IdentifyVal3);
        CheckEverything();
    }
    public void Val3Decrement()
    {
        UpdateText3(--IdentifyVal3);
        CheckEverything();
    }

    private void UpdateText3(int value) => Val3Text?.SetText(value.ToString());

    public void CheckGroup3()
    {
        Debug.Log("Check group 3");
        if (Neg2ItemsToggled == 0 && Neg6ItemsToggled == 0)
        {
            Debug.Log("3rd Group is correct");
            SelectNumber3.SetActive(true);
        }
        else
        {
            Debug.Log("Something is incorrect");
            if (gameObserver.replaytimes != 2)
            {
                gameObserver.ForceErroShow();
            }
        }
    }

    public void CheckEverything()
    {
        if (Neg2ItemsToggled == 0 && Neg6ItemsToggled == 0 && ThreeItemsToggled == 3 && Neg7ItemsToggled == 3 && Neg4ItemsToggled == 4 && FiveItemsToggled == 4 && IdentifyVal1 == 1 && IdentifyVal2 == -4 && IdentifyVal3 == -8)
        {
            gameObserver.itemsPlaced += 3;
            gameObserver.correctlyPlacedItems += 3;
            gameObserver.CheckallItemSlots();
            Debug.Log("Everything Should be complete");
        }
    }

    void Start()
    {
        Debug.Log("Hide them objects");
        SelectNumber1.SetActive(false);
        SelectNumber2.SetActive(false);
        SelectNumber3.SetActive(false);
    }


}
