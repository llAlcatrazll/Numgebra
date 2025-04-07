using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Intro_Game_Manager : MonoBehaviour
{
    [SerializeField] private Button female;
    [SerializeField] private Button male;
    [SerializeField] private GameObject playerSelect_screen;
    [SerializeField] private GameObject gameStory_screen;
    [SerializeField] private TextMeshProUGUI replayTimesText; // UI text to display total replay times

    private LevelAccounts levelAccounts;  // To store the deserialized data

    // Define the structure for your JSON data
    [System.Serializable]
    public class Level
    {
        public int level_id;
        public int replaytimes;
        public bool playable;
    }

    [System.Serializable]
    public class LevelAccounts
    {
        public List<Level> levels;  // This will hold the list of levels
    }

    // Start is called before the first frame update
    void Start()
    {
        female.onClick.AddListener(OnButtonClick);
        male.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Debug.Log("Player Gender Selected");
        playerSelect_screen.SetActive(false);
        gameStory_screen.SetActive(true);
    }

    public void displayStageProgression()
    {
        LoadLevelData();

        if (levelAccounts != null && levelAccounts.levels != null)
        {
            int totalReplayTimes = 0;

            foreach (Level level in levelAccounts.levels)
            {
                totalReplayTimes += level.replaytimes;
            }

            replayTimesText.text = "Total Replay Times: " + totalReplayTimes;
        }
        else
        {
            Debug.LogError("Level data is not loaded correctly.");
            replayTimesText.text = "Data Load Error!";
        }
    }

    void LoadLevelData()
    {
        TextAsset json = Resources.Load<TextAsset>("Database/Numgebra_DB");

        if (json != null)
        {
            levelAccounts = JsonUtility.FromJson<LevelAccounts>(json.ToString());
            Debug.Log("Level data loaded successfully!");
        }
        else
        {
            Debug.LogError("Failed to load Numgebra_DB JSON file from Resources!");
        }
    }
}
