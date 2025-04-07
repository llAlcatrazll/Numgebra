using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    private string filePath;
    private LevelAccounts levelAccounts; // Store level data in memory

    public static LevelLoader Instance { get; private set; } // Singleton for easy access

    [SerializeField] public GameObject FirstUnlock;
    [SerializeField] public GameObject SecondUnlock;
    [SerializeField] public GameObject ThirdUnlock;
    [SerializeField] public GameObject FourthUnlock;
    [SerializeField] public GameObject FifthUnlock;
    [SerializeField] public GameObject SixthUnlock;
    [SerializeField] private ScrollRect scrollRect;  // Reference to ScrollRect
    // CAMERA THING TO MOVE TO
    [SerializeField] private RectTransform content;  // The content inside the ScrollRect
    // THE ONES CONTAINING THE LEVELS AND MUST 0 DISTANCE TO SCROLLRECT
    // CONTENT = SCROLLRECT -> ADD STAGE TRANSFORM
    [SerializeField] private RectTransform[] levelTransforms = new RectTransform[35]; // ✅ 35 RectTransforms
    // TAKE X AND Y VALUES AND 
    [SerializeField] private MonoBehaviour[] levelScripts = new MonoBehaviour[35]; // ✅ 35 Level Scripts
    [SerializeField] private Camera mainCamera;  // Reference to main camera
    [SerializeField] public int defaultlevelIndex;
    [SerializeField] private int TargetIndex; // Level Index (for testing)
    private RectTransform objectOffset; // The RectTransform to align
    [SerializeField] private GameObject rewardObject;
    [SerializeField] private string playerGender; // Visible in Inspector
    public void SetTargetLevel(int levelIndex)
    {
        TargetIndex = levelIndex;
        Debug.Log($"Searching for level at index: {TargetIndex}");

        if (TargetIndex >= 0 && TargetIndex < levelTransforms.Length)
        {
            objectOffset = levelTransforms[TargetIndex];
            Debug.Log($"Found level at index {TargetIndex} with position X: {objectOffset.anchoredPosition.x}, Y: {objectOffset.anchoredPosition.y}");
            AlignContentToScrollRect();
        }
        else
        {
            Debug.LogError($"Invalid level index: {TargetIndex}. Must be between 0 and {levelTransforms.Length - 1}");
        }
    }


    public void AlignContentToScrollRect()
    {
        if (objectOffset == null)
        {
            Debug.LogError("objectOffset is null! Cannot align content.");
            return;
        }
        if (scrollRect == null)
        {
            Debug.LogError("[ERROR] scrollRect is NULL in AlignContentToScrollRect!");
            return;
        }
        if (content == null)
        {
            Debug.LogError("[ERROR] content is NULL in AlignContentToScrollRect!");
            return;
        }

        // 1. Get the RectTransform of the ScrollRect itself.
        RectTransform scrollRectRT = scrollRect.GetComponent<RectTransform>();
        // 2. Local position of the target level in the content.
        Vector2 targetLocalPos = objectOffset.anchoredPosition;

        // 3. Calculate the center of the ScrollRect in its own coordinate space.
        //    (If your pivot is not (0,0), you may need to adjust this logic.)
        // Vector2 scrollRectCenter = new Vector2(scrollRectRT.rect.width * 0.5f,
        //   scrollRectRT.rect.height * 0.5f);

        // 4. Position the content so that 'objectOffset' ends up in the ScrollRect center.
        // Vector2 newAnchoredPos = -targetLocalPos + scrollRectCenter;

        // 5. Assign the new position to the content's anchoredPosition.
        // content.anchoredPosition = newAnchoredPos;
        content.anchoredPosition = scrollRectRT.anchoredPosition;
        //         Vector2 newAnchoredPos = new Vector2(
        //     targetLocalPos.x > 0 ? -targetLocalPos.x : -targetLocalPos.x,
        //     targetLocalPos.y > 0 ? -targetLocalPos.y : -targetLocalPos.y
        // );
        content.anchoredPosition = -objectOffset.anchoredPosition;
        // STAGE 33 POSITION Vector3(604.410156,-1018.22375,0)
        // REVERSE THE VALUES
        // (Optional) If you want to clamp the scrolling so you don’t scroll beyond your map edges,
        // you can add a clamping step here, based on the content size vs. the ScrollRect size.
        // Debug.Log($"[AlignContentToScrollRect] Centering on {objectOffset.name} at anchoredPos {targetLocalPos}. " +
        //   $"New content pos: {newAnchoredPos}");
    }

    // public void AlignContentToScrollRect()
    // // THIS IS THE CAMERA REALIGNMENT
    // {
    //     if (objectOffset == null)
    //     {
    //         Debug.LogError("objectOffset is null! Cannot align content.");
    //         return;
    //     }
    //     if (scrollRect == null)
    //     {
    //         Debug.LogError("[ERROR] scrollRect is NULL in AlignContentToScrollRect!");
    //         return;
    //     }
    //     if (content == null)
    //     {
    //         Debug.LogError("[ERROR] content is NULL in AlignContentToScrollRect!");
    //         return;
    //     }


    //     // Get ScrollRect's position
    //     Vector2 scrollRectPos = scrollRect.GetComponent<RectTransform>().anchoredPosition;
    //     Debug.Log($"ScrollRect position: X: {scrollRectPos.x}, Y: {scrollRectPos.y}");

    //     // Apply offset by subtracting objectOffset's anchoredPosition
    //     content.anchoredPosition = scrollRectPos - objectOffset.anchoredPosition;
    //     Debug.Log($"Content new position: X: {content.anchoredPosition.x}, Y: {content.anchoredPosition.y}");
    // }
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Level_Selector") // Ensure we're in the correct scene
        {
            if (scrollRect == null)
            {
                scrollRect = GameObject.Find("2D_Game_Scroll_Area")?.GetComponent<ScrollRect>();
                if (scrollRect == null)
                    Debug.LogError("[ERROR] ScrollRect not found by 2D_Game_Scroll_Area name.");
            }

            if (content == null)
            {
                content = GameObject.Find("2D_Game_Levels")?.GetComponent<RectTransform>();
                if (content == null)
                    Debug.LogError("[ERROR] Content not found by 2D_Game_Levels name.");
            }

            AssignLevelTransforms(); // Only assign if in "Level_Selector" scene
        }
    }

    private void AssignLevelTransforms()
    {
        // Ensure the array is initialized before assigning values
        if (levelTransforms == null || levelTransforms.Length != 35)
            levelTransforms = new RectTransform[35];

        for (int i = 0; i < levelTransforms.Length; i++)
        {
            string levelName = $"lvl_{i + 1}"; // Construct the object name
            GameObject levelObject = GameObject.Find(levelName);

            if (levelObject != null)
            {
                levelTransforms[i] = levelObject.GetComponent<RectTransform>();
                if (levelTransforms[i] == null)
                    Debug.LogError($"[ERROR] {levelName} is missing a RectTransform component!");
            }
            else
            {
                Debug.LogError($"[ERROR] GameObject '{levelName}' not found in the scene!");
            }
        }
    }
    private void Awake()
    {
        HandleSingleton();
        filePath = Path.Combine(Application.persistentDataPath, "Numgebra_DB.json");
        EnsureJsonExists();
        EnsurePlayerDataExists(); // ✅ Add player data if missing
        LoadLevelData();
        // Assign scrollRect and content immediately
        if (scrollRect == null)
            scrollRect = GameObject.Find("2D_Game_Scroll_Area")?.GetComponent<ScrollRect>();

        if (content == null)
            content = GameObject.Find("2D_Game_Levels")?.GetComponent<RectTransform>();

        if (scrollRect == null || content == null)
            Debug.LogError("[ERROR] ScrollRect or Content is missing!");

        mainCamera = Camera.main; // 🔥 Assign camera
        SceneManager.sceneLoaded += OnSceneLoaded;


    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level_Selector")
        {
            Debug.Log("Level_Selector scene loaded. Reinitializing components.");

            mainCamera = Camera.main;
            AssignLevelTransforms();

            // 🔥 Ensure scrollRect and content exist before proceeding
            if (scrollRect == null)
                scrollRect = GameObject.Find("2D_Game_Scroll_Area")?.GetComponent<ScrollRect>();
            if (content == null)
                content = GameObject.Find("2D_Game_Levels")?.GetComponent<RectTransform>();

            if (scrollRect == null || content == null)
            {
                Debug.LogError("[ERROR] ScrollRect or Content is still null! Delaying target level setting...");
                StartCoroutine(WaitAndSetTargetLevel());
                return;
            }

            if (PlayerPrefs.HasKey("TargetStageIndex"))
            {
                int savedIndex = PlayerPrefs.GetInt("TargetStageIndex");
                PlayerPrefs.DeleteKey("TargetStageIndex");
                Debug.Log($"Setting camera position to stage {savedIndex}");
                SetTargetLevel(savedIndex);
            }
        }
    }
    private IEnumerator WaitAndSetTargetLevel()
    {
        yield return new WaitUntil(() => scrollRect != null && content != null);
        int savedIndex = PlayerPrefs.GetInt("TargetStageIndex", 0);
        Debug.Log($"[INFO] Retrying setting camera position to stage {savedIndex}");
        SetTargetLevel(savedIndex);
    }




    private void OnDestroy()
    {
        Debug.Log("SCENE DESTROYED");
        SceneManager.sceneLoaded -= OnSceneLoaded; // Prevent memory leaks
    }
    // private void OnEnable()
    // {
    //     Debug.Log("Scene is enabled")
    //     if (PlayerPrefs.HasKey("TargetStageIndex"))
    //     {
    //         int savedIndex = PlayerPrefs.GetInt("TargetStageIndex");
    //         PlayerPrefs.DeleteKey("TargetStageIndex"); // Remove it after using

    //         Debug.Log($"Updating Level Selector with saved stage index: {savedIndex}");
    //         SetTargetLevel(savedIndex);
    //     }
    //     {
    //         SetTargetLevel(defaultlevelIndex); // Default to initial level
    //     }
    // }
    private void Start()
    {

        // SetTargetLevel(defaultlevelIndex);
        // if (PlayerPrefs.HasKey("TargetStageIndex"))
        // {
        //     int savedIndex = PlayerPrefs.GetInt("TargetStageIndex");
        //     PlayerPrefs.DeleteKey("TargetStageIndex"); // Remove it after using

        //     Debug.Log($"Loading Level Selector with saved stage index: {savedIndex}");
        //     SetTargetLevel(savedIndex);
        // }
        // else
        // {
        //     SetTargetLevel(defaultlevelIndex); // Default to initial level
        // }
        string gender = LoadPlayerGenderFromJson();
        Local_Observer.SetGender(gender); // Send gender to local_observer
        CheckAndUnlockLevels();
        GetTotalGasCansandDisplay100();
    }
    public int GetTotalGasCansandDisplay100()
    {
        if (levelAccounts == null || levelAccounts.levels == null)
        {
            Debug.LogError("[ERROR] Level data is null or empty!");
            return 0;
        }

        int totalGasCans = 0;
        foreach (var level in levelAccounts.levels)
        {
            totalGasCans += level.stars; // Assuming 'stars' represents gas cans
        }

        // Check if total stars reach 100
        if (totalGasCans >= 100)
        {
            Debug.Log("[REWARD] Congratulations! You have collected 100 stars!");
            if (rewardObject != null)
            {
                rewardObject.SetActive(true); // Enable reward object
                Debug.Log("[REWARD] Reward object has been enabled.");
            }
            else
            {
                Debug.LogError("[ERROR] Reward object is not assigned!");
            }
        }

        return totalGasCans;
    }
    private void CheckAndUnlockLevels()
    {
        if (levelAccounts == null || levelAccounts.levels == null)
        {
            Debug.LogError("[ERROR] Level data is null or empty!");
            return;
        }

        StartCoroutine(UnlockSequence());
    }

    private IEnumerator UnlockSequence()
    {
        int delay = 0;
        foreach (var level in levelAccounts.levels)
        {
            if (level.stars > 0)
            {
                switch (level.level_id)
                {
                    case 5:
                        StartCoroutine(DisableUnlockObject(FirstUnlock, delay));
                        delay += 1;
                        break;
                    case 10:
                        StartCoroutine(DisableUnlockObject(SecondUnlock, delay));
                        delay += 1;
                        break;
                    case 15:
                        StartCoroutine(DisableUnlockObject(ThirdUnlock, delay));
                        delay += 1;
                        break;
                    case 20:
                        StartCoroutine(DisableUnlockObject(FourthUnlock, delay));
                        delay += 1;
                        break;
                    case 25:
                        StartCoroutine(DisableUnlockObject(FifthUnlock, delay));
                        delay += 1;
                        break;
                    case 30:
                        StartCoroutine(DisableUnlockObject(SixthUnlock, delay));
                        delay += 1;
                        break;
                }
            }
        }
        yield return null;
    }

    private IEnumerator DisableUnlockObject(GameObject unlockObject, int delay)
    {
        yield return new WaitForSeconds(delay);
        if (unlockObject != null)
        {
            unlockObject.SetActive(false);
            yield return new WaitForSeconds(5);
            // unlockObject.SetActive(true);
        }
    }

    public MonoBehaviour GetLevelScript(int index)
    {
        if (index >= 0 && index < levelScripts.Length && levelScripts[index] != null)
            return levelScripts[index];

        Debug.LogError($"[ERROR] No script found at index {index}");
        return null;
    }

    public RectTransform GetLevelTransform(int index)
    {
        if (index >= 0 && index < levelTransforms.Length && levelTransforms[index] != null)
            return levelTransforms[index];

        Debug.LogError($"[ERROR] No RectTransform found at index {index}");
        return null;
    }

    public void ResetLevels()
    {
        if (levelAccounts == null || levelAccounts.levels == null)
        {
            Debug.LogError("[ERROR] Level data is null or empty!");
            return;
        }

        foreach (var level in levelAccounts.levels)
        {
            level.stars = 0; // Reset stars to 0
            // level.playable = (level.level_id == 1); // Only level with id 1 remains playable
        }

        SaveLevelData();
        Debug.Log("[RESET] All levels reset! Only Level 1 is playable.");
    }

    private string LoadPlayerGenderFromJson()
    {
        if (levelAccounts != null && levelAccounts.player != null && levelAccounts.player.Length > 0)
        {
            return levelAccounts.player[0].gender;
        }
        Debug.LogError("[ERROR] Failed to retrieve player gender from JSON!");
        return "unknown"; // Fallback value
    }

    private void HandleSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this manager persistent across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void EnsurePlayerDataExists()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            levelAccounts = JsonUtility.FromJson<LevelAccounts>(json);

            if (levelAccounts.player == null || levelAccounts.player.Length == 0)
            {
                Debug.LogWarning("[WARNING] No player data found! Adding default player data...");

                levelAccounts.player = new Player[] { new Player { gender = "male" } };

                SaveLevelData();
                Debug.Log("[UPDATE] Player data added and saved!");
            }
            else
            {
                Debug.Log("[CHECK] Player data already exists.");
            }
        }
        else
        {
            Debug.LogError("[ERROR] JSON file missing! Cannot modify.");
        }
    }

    private void EnsureJsonExists()
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("[CREATE] No JSON found. Copying from Resources.");

            TextAsset jsonFile = Resources.Load<TextAsset>("Numgebra_DB");
            if (jsonFile != null)
            {
                File.WriteAllText(filePath, jsonFile.text);
                Debug.Log("[COPY] JSON copied to persistentDataPath.");
            }
            else
            {
                Debug.LogError("[ERROR] Failed to load JSON from Resources!");
            }
        }
        else
        {
            Debug.Log("[CHECK] JSON already exists.");
        }
    }

    private void LoadLevelData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            levelAccounts = JsonUtility.FromJson<LevelAccounts>(json);

            if (levelAccounts == null)
            {
                Debug.LogError("[ERROR] Failed to parse level data!");
                return;
            }

            if (levelAccounts.player != null && levelAccounts.player.Length > 0)
            {
                Debug.Log($"[INFO] Player Gender: {levelAccounts.player[0].gender}");
            }
            else
            {
                Debug.LogWarning("[WARNING] No player data found in JSON!");
            }

            Debug.Log($"[LOAD] Loaded {levelAccounts.levels.Length} levels.");

            // ✅ Log all 35 level stars with new lines for better readability
            string starLog = "[LEVEL STARS]\n";
            for (int i = 0; i < levelAccounts.levels.Length; i++)
            {
                starLog += $"Lvl {i + 1} - {levelAccounts.levels[i].stars} stars\n";
            }

            Debug.Log(starLog.TrimEnd()); // Ensure no extra newline at the end
        }
        else
        {
            Debug.LogError("[ERROR] JSON file missing!");
        }
    }



    public LevelAccounts GetLevelData()
    {
        return levelAccounts;
    }

    public int GetTotalGasCans()
    {
        if (levelAccounts == null || levelAccounts.levels == null)
        {
            Debug.LogError("[ERROR] Level data is null or empty!");
            return 0;
        }

        int totalGasCans = 0;
        foreach (var level in levelAccounts.levels)
        {
            totalGasCans += level.stars; // Assuming 'stars' represents gas cans
        }

        return totalGasCans;
    }

    public void SaveLevelData()
    {
        if (levelAccounts != null)
        {
            string json = JsonUtility.ToJson(levelAccounts, true);
            File.WriteAllText(filePath, json);
            Debug.Log("[SAVE] Level data saved!");
        }
        else
        {
            Debug.LogError("[ERROR] No level data to save!");
        }
    }

    public void SetPlayerGenderFemale()
    {
        if (levelAccounts != null && levelAccounts.player != null && levelAccounts.player.Length > 0)
        {
            levelAccounts.player[0].gender = "female";
            SaveLevelData();
            Debug.Log($"[UPDATE] Player gender set to: {levelAccounts.player[0].gender}");
        }
        else
        {
            Debug.LogError("[ERROR] No player data found to update!");
        }
    }

    public void SetPlayerGenderMale()
    {
        if (levelAccounts != null && levelAccounts.player != null && levelAccounts.player.Length > 0)
        {
            levelAccounts.player[0].gender = "male";
            SaveLevelData();
            Debug.Log($"[UPDATE] Player gender set to: {levelAccounts.player[0].gender}");
        }
        else
        {
            Debug.LogError("[ERROR] No player data found to update!");
        }
    }


}
