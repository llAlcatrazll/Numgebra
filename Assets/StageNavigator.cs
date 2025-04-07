using UnityEngine;
using UnityEngine.SceneManagement;

public class StageNavigator : MonoBehaviour
{
    public void ReturnToLevelSelector()
    {
        // Find Local_Observer in the scene
        Local_Observer observer = FindObjectOfType<Local_Observer>();

        if (observer != null)
        {
            // Store the stage number in PlayerPrefs before switching scenes
            PlayerPrefs.SetInt("TargetStageIndex", observer.stageNumber - 1);
            PlayerPrefs.Save(); // Save PlayerPrefs to persist data

            Debug.Log($"Saving stage number: {observer.stageNumber} and switching to Level_Selector");
        }
        else
        {
            Debug.LogError("Local_Observer not found in the scene!");
        }

        // Load Level_Selector scene
        SceneManager.LoadScene("Level_Selector");
    }
}
