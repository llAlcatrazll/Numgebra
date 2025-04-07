using System.Collections;
using UnityEngine;
using TMPro;

public class SoloIdentifyGameObserver : MonoBehaviour
{
    [SerializeField] public TMP_Text CheckAbsolValue;
    [SerializeField] private Local_Observer gameObserver;
    [SerializeField] public int AbsolValueAnswer;
    [SerializeField] private GameObject TreeLeaves;
    [SerializeField] private bool isFlames;
    [SerializeField] private GameObject ErrorMessage;
    [SerializeField] private float fadeDuration = 1.0f; // 1-second fade
    public bool isAlreadyCorrect = false;
    private CanvasGroup treeCanvasGroup;

    private void Start()
    {
        // Add CanvasGroup component if not already attached
        treeCanvasGroup = TreeLeaves.GetComponent<CanvasGroup>();
        if (treeCanvasGroup == null)
        {
            treeCanvasGroup = TreeLeaves.AddComponent<CanvasGroup>();
        }
    }

    public void CheckGameComplete()
    {
        int Value = ConvertTextToInt(CheckAbsolValue);
        Debug.Log($"Value Check: {Value}");

        if (Value == AbsolValueAnswer && !isAlreadyCorrect)
        {
            isAlreadyCorrect = true;
            gameObserver.itemsPlaced++;
            gameObserver.correctlyPlacedItems++;
            gameObserver.CheckallItemSlots();
            Debug.Log("GameComplete");
            GetComponent<UnityEngine.UI.Button>().interactable = false;
            Debug.Log("Disable button");
            if (isFlames)
            {
                StartCoroutine(FadeOut(TreeLeaves));
            }
            else
            {
                StartCoroutine(FadeIn(TreeLeaves));
            }
        }
        else
        {
            if (gameObserver.replaytimes == 1)
            {
                gameObserver.itemsPlaced += 3;
                gameObserver.CheckallItemSlots();
                gameObserver.incorrectInput.SetActive(false);

            }
            ErrorMessage.SetActive(true);
            gameObserver.replaytimes++;
            Debug.Log("Game Not Yet Complete.");


        }
    }

    private int ConvertTextToInt(TMP_Text textElement)
    {
        if (textElement != null && int.TryParse(textElement.text, out int value))
        {
            return value;
        }
        else
        {
            Debug.LogError($"Invalid integer format in {textElement?.name}");
            return 0;
        }
    }

    private IEnumerator FadeIn(GameObject obj)
    {
        obj.SetActive(true);
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            treeCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        treeCanvasGroup.alpha = 1;
    }

    private IEnumerator FadeOut(GameObject obj)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            treeCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        treeCanvasGroup.alpha = 0;
        obj.SetActive(false);
    }
}
