using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Local_Observer : MonoBehaviour
{
    [SerializeField] public GameObject firstStar;
    [SerializeField] public GameObject secondStar;
    [SerializeField] public GameObject thirdStar;
    [SerializeField] public GameObject introInstructions;
    [SerializeField] public GameObject tipScreen;
    [SerializeField] public GameObject gameCompleteScreen;
    [SerializeField] public GameObject grayDarkBG;
    [SerializeField] public GameObject tipButton;
    [SerializeField] public bool isClickGame;
    [SerializeField] public bool isAnimationEnding;
    [SerializeField] public bool isCave;
    [SerializeField] public MultiscrollLockObserver CaveObserver;
    [SerializeField] public bool isAngryBird;
    [SerializeField] public bool isPadlockCave;
    [SerializeField] public GameObject PadlockInstructions;
    [SerializeField] public bool isBackflipAnim;
    [SerializeField] private PlayerJump jumpingScriptAnim;
    [SerializeField] public GameObject caveBackground;
    [SerializeField] public GameObject incorrectInput;
    [SerializeField] public GameObject maleCharacter;
    [SerializeField] public GameObject femaleCharacter;
    public int replaytimes = 0;
    public int itemsPlaced = 0;
    public int correctlyPlacedItems = 0;
    [SerializeField] private int totalSlots;
    [SerializeField] public int stageNumber;

    [System.Serializable]
    public class Level
    {
        public int level_id;
        public bool playable;
        public int stars;
        public int replaytimes;
    }

    [System.Serializable]
    public class LevelAccounts
    {
        public List<Level> levels;
    }
    private static string playerGender;
    private LevelAccounts levelAccounts;

    public void Start()
    {
        EnableGender();
        checkCharGender();
        // Debug.Log("Chest char gender?...");
        // introInstructions.SetActive(true);
        // TO PREVENT BUGS LETS DISABLE THE SET ACTIVE FOR NOW AND JUST MAKE IT ACTIVE ON DEFAULT


        // StartCoroutine(HideIntroAfterDelay(5f)); 
        // Hides after 3 seconds
        // Debug.Log($"[local_observer] Player Gender: {playerGender}");

    }
    public static void SetGender(string gender)
    {
        playerGender = gender;
    }

    public void EnableGender()
    {
        if (playerGender == "male")
        {
            maleCharacter.SetActive(true);
            femaleCharacter.SetActive(false);
        }
        else
        {
            maleCharacter.SetActive(false);
            femaleCharacter.SetActive(true);
        }
    }

    public void checkCharGender()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "Numgebra_DB.json");

        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Numgebra_DB.json not found! Copying from Resources...");
            CopyJsonFromResources(filePath);
        }

        string json = File.ReadAllText(filePath);
        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("Failed to load JSON: File is empty.");
            return;
        }

        try
        {
            var jsonObject = JsonUtility.FromJson<LevelAccounts>(json);
            if (jsonObject != null && jsonObject.levels != null)
            {
                Debug.Log("[SUCCESS] JSON loaded and parsed.");
                Debug.Log($"Player Gender: {playerGender}");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[ERROR] Failed to parse JSON: {e.Message}");
        }
    }


    private IEnumerator HideIntroAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        introInstructions.SetActive(false);
    }

    public void oneStar()
    {
        firstStar.SetActive(true);
    }

    public void twoStar()
    {
        oneStar();
        secondStar.SetActive(true);
    }

    public void thriceStar()
    {
        oneStar();
        twoStar();
        thirdStar.SetActive(true);
    }
    public void GrasshopperStageComplete()
    {
        Debug.Log("Stage Complete Update JSON");
        UpdateStageComplete(stageNumber, CalculateStars());
        Debug.Log($"Stage Number: {stageNumber} + Stars: {CalculateStars()}");
    }
    public void StageComplete()
    {
        Debug.Log("Stage Complete is Being Called");
        grayDarkBG.SetActive(true);
        if (replaytimes < 1)
        {
            thriceStar();
        }
        if (replaytimes >= 3)
        {
            oneStar();
        }
        else
        {
            twoStar();
        }

        // Call UpdateStageComplete to update the JSON file
        UpdateStageComplete(stageNumber, CalculateStars());
        Debug.Log($"Stage Number: {stageNumber} + Stars: {CalculateStars()}");
    }

    private int CalculateStars()
    {
        if (replaytimes < 1)
        {
            return 3;
        }
        else if (replaytimes >= 3)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    public void UpdateStageComplete(int completedLevelId, int newStars)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "Numgebra_DB.json");

        // Ensure file exists
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Numgebra_DB.json not found! Copying from Resources...");
            CopyJsonFromResources(filePath);
        }

        Debug.Log($"Loading JSON from: {filePath}");

        // Read JSON file
        string json = File.ReadAllText(filePath);
        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("Failed to load JSON: File is empty.");
            return;
        }

        Debug.Log("Loaded JSON: " + json);

        // Deserialize JSON
        levelAccounts = JsonUtility.FromJson<LevelAccounts>(json);
        if (levelAccounts == null || levelAccounts.levels == null)
        {
            Debug.LogError("Error parsing JSON: LevelAccounts object is null.");
            return;
        }

        bool levelUpdated = false;

        // Find and update the level
        foreach (var level in levelAccounts.levels)
        {
            if (level.level_id == completedLevelId)
            {
                int previousStars = level.stars;
                level.stars = Mathf.Max(level.stars, newStars); // Keep the highest star count
                level.replaytimes += 1;

                Debug.Log($"Updated Level {completedLevelId}: Stars {previousStars} -> {level.stars}, Replaytimes: {level.replaytimes}");
                levelUpdated = true;
            }

            // Unlock next level
            if (level.level_id == completedLevelId + 1 && !level.playable)
            {
                level.playable = true;
                Debug.Log($"Level {completedLevelId + 1} is now unlocked!");
            }
        }

        if (!levelUpdated)
        {
            Debug.LogError($"Level {completedLevelId} not found in JSON.");
            return;
        }

        // Serialize updated object
        string updatedJson = JsonUtility.ToJson(levelAccounts, true);
        Debug.Log("Updated JSON: " + updatedJson);

        // Write back to file
        try
        {
            File.WriteAllText(filePath, updatedJson);
            Debug.Log($"Successfully wrote to Numgebra_DB.json at {filePath}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to write JSON: {ex.Message}");
        }
    }



    // Copies JSON from Resources to Persistent Data Path
    private void CopyJsonFromResources(string filePath)
    {
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


    public void RestartStage()
    {
        Debug.Log("Game is Being Restarted");
        DraggableReset();
        replaytimes = 0;
        itemsPlaced = 0;
        correctlyPlacedItems = 0;
    }
    public void CheckallItemSlots()
    {
        if (isClickGame && replaytimes == 2 || isAngryBird && replaytimes == 2)
        {
            tipScreen.SetActive(true);
            tipButton.SetActive(true);
        }

        Debug.Log("Check Item Slots is being called");
        if (isPadlockCave && itemsPlaced == 3 && correctlyPlacedItems == 3)
        {
            CaveObserver.CheckisLockCanUnlock();
        }
        if (isPadlockCave && itemsPlaced >= 3 && correctlyPlacedItems < 3)
        {
            Debug.Log("Is Padlock Cave Called and is wrong");
            DraggableReset();
            incorrectInput.SetActive(true);
        }
        if (itemsPlaced == totalSlots)
        // CLEAR THIS CONDITION FIRST SO THAT U CAN HAVE ERROR INPUT
        {
            Debug.Log("Check Items");

            if (isPadlockCave)
            {
                CaveObserver.CheckisLockCanUnlock();
                // Require 4 correctly placed items to complete
                if (correctlyPlacedItems >= 4)
                {
                    Debug.Log("Padlock Cave Completed");

                    if (isAnimationEnding)
                    {
                        StartCoroutine(WaitAndFinishLevel());
                    }
                    else
                    {
                        FinishLevel();
                    }
                }
                else
                {
                    Debug.Log("Padlock Cave: Not all required items placed yet.");
                    ForceErroShow();
                    replaytimes++;
                    DraggableReset();
                }
            }
            else
            {
                // Regular game logic
                if (correctlyPlacedItems >= itemsPlaced)
                {
                    Debug.Log("Stage Completed gameObserver");

                    if (isAnimationEnding)
                    {
                        StartCoroutine(WaitAndFinishLevel());
                    }
                    else
                    {
                        FinishLevel();
                    }
                }
                else if (correctlyPlacedItems < itemsPlaced && replaytimes == 1)
                {
                    tipScreen.SetActive(true);
                    tipButton.SetActive(true);
                    replaytimes++;
                    DraggableReset();
                }
                else
                {
                    Debug.Log("Check Incorrect Input");
                    ForceErroShow();
                    replaytimes++;
                    DraggableReset();
                }
            }
        }

        if (isClickGame && replaytimes == 2)
        {
            tipScreen.SetActive(true);
            tipButton.SetActive(true);
        }
    }

    // public void CheckallItemSlots()
    // {
    //     if (isClickGame && replaytimes == 2)
    //     {
    //         tipScreen.SetActive(true);
    //         tipButton.SetActive(true);
    //     }
    //     Debug.Log("Check Item Slots is being called");
    //     if (itemsPlaced == totalSlots)
    //     {
    //         Debug.Log("Check Items");
    //         if (correctlyPlacedItems >= itemsPlaced)
    //         {
    //             Debug.Log("Stage Completed gameObserver");

    //             // If animation is ending, wait for 1 second before finishing the level
    //             if (isAnimationEnding)
    //             {
    //                 StartCoroutine(WaitAndFinishLevel());
    //             }
    //             else
    //             {
    //                 FinishLevel();
    //             }
    //         }
    //         else if (correctlyPlacedItems < itemsPlaced && replaytimes == 1)
    //         {
    //             // SHOW TIP SCREEN
    //             tipScreen.SetActive(true);
    //             tipButton.SetActive(true);
    //             replaytimes++;
    //             DraggableReset();
    //         }
    //         else
    //         {
    //             Debug.Log("Check Incorrect Input");
    //             ForceErroShow();
    //             replaytimes++;
    //             DraggableReset();
    //         }
    //     }
    //     if (isClickGame && replaytimes == 2)
    //     {
    //         tipScreen.SetActive(true);
    //         tipButton.SetActive(true);
    //     }
    // }
    // Coroutine to wait for 1 second before finishing the level
    private IEnumerator WaitAndFinishLevel()
    {
        if (isCave)
        {
            StartCoroutine(ChangeCaveBackgroundColor());
            yield return new WaitForSeconds(1f);
            FinishLevel();
        }
        if (isBackflipAnim)
        {
            jumpingScriptAnim.StartJump();
            yield return new WaitForSeconds(1.6f);
            FinishLevel();
        }
        else
        {
            yield return new WaitForSeconds(1f);
            FinishLevel();
        }

    }

    // Function to complete the level
    private void FinishLevel()
    {
        gameCompleteScreen.SetActive(true);
        grayDarkBG.SetActive(true);
        StageComplete();
    }
    private IEnumerator ChangeCaveBackgroundColor()
    {
        if (caveBackground != null)
        {
            RawImage caveRawImage = caveBackground.GetComponent<RawImage>();
            if (caveRawImage != null)
            {
                Color startColor = new Color(63f / 255f, 63f / 255f, 63f / 255f, 1f); // #3F3F3F
                Color endColor = Color.white;
                float duration = 1f;
                float elapsed = 0f;

                while (elapsed < duration)
                {
                    elapsed += Time.deltaTime;
                    caveRawImage.color = Color.Lerp(startColor, endColor, elapsed / duration);
                    yield return null;
                }

                caveRawImage.color = endColor; // Ensure final color is set
            }
            else
            {
                Debug.LogError("Cave background does not have a RawImage component.");
            }
        }
        else
        {
            Debug.LogError("Cave background GameObject is not assigned.");
        }
    }
    public void ForceErroShow()
    {
        incorrectInput.SetActive(true);
    }
    public void ForceGameComplete_Mode()
    {
        Debug.Log("Game will now finish");
    }

    public void ForceTipScreen()
    {
        if (replaytimes == 2)
        {
            tipScreen.SetActive(true);
            tipButton.SetActive(true);
            DraggableReset();
        }
    }

    public void DraggableReset()
    {
        Draggable_Item[] allDraggableItems = FindObjectsOfType<Draggable_Item>();
        itemsPlaced = 0;
        correctlyPlacedItems = 0;
        foreach (Draggable_Item draggableItem in allDraggableItems)
        {
            if (draggableItem.isPlaced && !draggableItem.isCorrectlyPlaced)
            {
                draggableItem.ResetPosition();
                draggableItem.itemSlotID = 99999;
            }
            if (draggableItem.isCorrectlyPlaced)
            {
                correctlyPlacedItems++;
                itemsPlaced++;
            }
        }
    }
}
