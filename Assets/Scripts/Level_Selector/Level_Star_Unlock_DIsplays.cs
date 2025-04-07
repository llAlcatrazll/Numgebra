using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;
public class Level_Star_Unlock_Displays : MonoBehaviour
{
    [SerializeField] public int levelID; // ID of this level in the UI
    [SerializeField] private GameObject firstStar;
    [SerializeField] private GameObject secondStar;
    [SerializeField] private GameObject thirdStar;
    [SerializeField] private GameObject stageLock;
    [SerializeField] private Button stageButton;
    [SerializeField] private TMP_Text stageNumber;
    private int starsForLevel = 0;
    private bool isPlayable = false;

    void Start()
    {
        // Debug.Log($"[START] Level ID (Serialized): {levelID}");

        if (levelID < 1) // Ensure level ID is valid
        {
            Debug.LogError($"[ERROR] Level ID {levelID} is invalid (must be 1 or higher). Skipping...");
            return;
        }

        LoadLevelData();
        // SetupUI();
        stageLock.SetActive(false);
        InvokeRepeating(nameof(UpdateStars), 1f, 1f);
    }
    void UpdateStars()
    {
        LoadLevelData();
        EnableStars();
    }

    void LoadLevelData()
    {
        LevelAccounts levelAccounts = LevelLoader.Instance.GetLevelData();

        if (levelAccounts == null || levelAccounts.levels == null)
        {
            Debug.LogError("[ERROR] Failed to load level data from JSON!");
            return;
        }

        // Convert the array to a List, sort, then convert back if needed
        var sortedLevels = levelAccounts.levels.ToList(); // Convert to List
        sortedLevels.Sort((a, b) => a.level_id.CompareTo(b.level_id)); // Sort using CompareTo

        foreach (Level level in sortedLevels)
        {
            if (level.level_id == levelID)
            {
                starsForLevel = level.stars;
                isPlayable = level.playable;
                // Debug.Log($"[FOUND] Level {levelID}: Stars = {starsForLevel}, Playable = {isPlayable}");
                break;
            }
        }
    }


    // void SetupUI()
    // {
    //     stageLock.SetActive(false);
    //     EnableStars();
    // }

    void EnableStars()
    {
        // Ensure stars are hidden before setting them
        firstStar.SetActive(false);
        secondStar.SetActive(false);
        thirdStar.SetActive(false);

        // Show stars based on starsForLevel value
        if (starsForLevel >= 1)
        {
            // Debug.Log("First Star Enableed");
            firstStar.SetActive(true);
        }

        if (starsForLevel >= 2)
        {
            // Debug.Log("Second Star Enabled");
            secondStar.SetActive(true);
        }
        if (starsForLevel >= 3)
        {
            thirdStar.SetActive(true);

            // Debug.Log("Third Star Enabled");
        }

        // Debug.Log($"[STARS] Level {levelID} - Stars Displayed: {starsForLevel}");
    }

    void SetButtonInteractable()
    {
        stageButton.interactable = false;
    }

    // void SetStageTextDark()
    // {
    //     stageNumber.color = new Color(144f / 255f, 144f / 255f, 144f / 255f, 1f);
    // }

    // void SetLockEnabled()
    // {
    //     stageLock.SetActive(true);
    // }
}
