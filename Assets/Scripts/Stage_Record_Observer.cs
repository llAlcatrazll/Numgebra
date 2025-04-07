using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Record_Observer : MonoBehaviour
{

    [SerializeField] private GameObject First_Star;
    [SerializeField] private GameObject Second_Star;
    [SerializeField] private GameObject Third_Star;
    [SerializeField] private DragnDrop_GameObserver gameObserver;
    [SerializeField] private Grasshopper_GameObserver gasshopObserver;

    [SerializeField] public GameObject TipScreen;
    [SerializeField] public GameObject TipButton;

    [SerializeField] public Local_Observer updateObserver;
    public int GrasshopperReplayTimes = 0;
    public void StageComplete()
    {
        if (gameObserver.replaytimes == 0)
        {
            Debug.Log("3stars");
            // 
            Debug.Log("LEVEL LOADER LOG STARS");
            First_Star.SetActive(true);
            Second_Star.SetActive(true);
            Third_Star.SetActive(true);
        }
        if (gameObserver.replaytimes >= 3)
        {
            Debug.Log("1star");
            First_Star.SetActive(true);
        }
        else
        {
            Debug.Log("2stars");
            First_Star.SetActive(true);
            Second_Star.SetActive(true);
            Third_Star.SetActive(false);
        }
    }
    public void GrassHopperStageComplete()
    {
        Debug.Log("Stars are being callled properly");
        if (GrasshopperReplayTimes == 0)
        {
            updateObserver.replaytimes = 0;
            updateObserver.GrasshopperStageComplete();
            Debug.Log("3stars");
            Debug.Log(GrasshopperReplayTimes);
            First_Star.SetActive(true);
            Second_Star.SetActive(true);
            Third_Star.SetActive(true);
        }
        if (GrasshopperReplayTimes >= 3)
        {
            updateObserver.replaytimes = 3;
            updateObserver.GrasshopperStageComplete();
            Debug.Log("1star");
            First_Star.SetActive(true);
        }
        else
        {
            updateObserver.replaytimes = 2;
            updateObserver.GrasshopperStageComplete();
            Debug.Log("2stars");
            First_Star.SetActive(true);
            Second_Star.SetActive(true);
        }
    }
}