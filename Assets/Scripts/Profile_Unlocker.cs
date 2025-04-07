using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Profile_Unlocker : MonoBehaviour
{
    [SerializeField] private int levelID; // ID of this level in the UI
    [SerializeField] private GameObject firstStar;
    [SerializeField] private GameObject secondStar;
    [SerializeField] private GameObject thirdStar;
    [SerializeField] private Button stageButton;
    [SerializeField] private TMP_Text stageNumber;

    private int starsForLevel = 0;
    private bool isPlayable = false;

    void Start()
    {
        if (levelID < 1) // Ensure level ID is valid
        {
            Debug.LogError($"[ERROR] Level ID {levelID} is invalid (must be 1 or higher). Skipping...");
            return;
        }

        try
        {
            LoadLevelData();
            SetupUI();
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[CRITICAL ERROR] Profile_Unlocker encountered an error: {ex.Message}\n{ex.StackTrace}");
        }
    }

    void LoadLevelData()
    {
        if (LevelLoader.Instance == null)
        {
            Debug.LogError("[ERROR] LevelLoader instance is missing! Cannot load level data.");
            return;
        }

        LevelAccounts levelAccounts = LevelLoader.Instance.GetLevelData();

        if (levelAccounts == null || levelAccounts.levels == null)
        {
            Debug.LogError("[ERROR] Failed to load level data from JSON!");
            return;
        }

        foreach (Level level in levelAccounts.levels)
        {
            if (level.level_id == levelID)
            {
                starsForLevel = level.stars;
                isPlayable = level.playable;
                break;
            }
        }
    }

    void SetupUI()
    {
        if (!isPlayable)
        {
            Debug.Log($"[INFO] Level {levelID} is not playable.");
        }
        else
        {
            EnableStars();
        }
    }

    void EnableStars()
    {
        if (firstStar == null || secondStar == null || thirdStar == null)
        {
            Debug.LogWarning("[WARNING] One or more star GameObjects are missing! Skipping star display.");
            return;
        }

        firstStar.SetActive(false);
        secondStar.SetActive(false);
        thirdStar.SetActive(false);

        if (starsForLevel >= 1) firstStar.SetActive(true);
        if (starsForLevel >= 2) secondStar.SetActive(true);
        if (starsForLevel >= 3) thirdStar.SetActive(true);
    }
}
