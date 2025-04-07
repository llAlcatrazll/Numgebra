using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIInteractionFix : MonoBehaviour
{
    private void Start()
    {
        EnsureEventSystem();
        EnableAllButtons();
        DebugRaycastBlockers();

        // Re-run every 2 seconds
        InvokeRepeating(nameof(RunUIFixes), 2f, 2f);
    }

    private void RunUIFixes()
    {
        EnableAllButtons();
        DebugRaycastBlockers();
        // Debug.Log("check");
    }

    private void EnsureEventSystem()
    {
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
            // Debug.LogWarning("No EventSystem found! Created a new one.");
        }
    }

    private void EnableAllButtons()
    {
        Button[] buttons = FindObjectsOfType<Button>(true);
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
        // Debug.Log($"Re-enabled {buttons.Length} buttons in the scene.");
    }

    private void DebugRaycastBlockers()
    {
        List<RaycastResult> results = new List<RaycastResult>();
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = new Vector2(Screen.width / 2, Screen.height / 2);

        EventSystem.current.RaycastAll(pointerEventData, results);
        if (results.Count > 0)
        {
            foreach (RaycastResult result in results)
            {
                // Debug.LogWarning($"UI Raycast blocker detected: {result.gameObject.name}", result.gameObject);
            }
        }
    }
}
