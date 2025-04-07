using System;
using System.Collections.Generic;
using UnityEngine;

public class CatchSceneErrors : MonoBehaviour
{
    private HashSet<string> suppressedErrors = new HashSet<string>
    {
        "Profile_Unlocker.LoadLevelData",
        "Profile_Unlocker.Start"
    };

    void Awake()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void Start()
    {
        try
        {
            InitializeScene();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Caught Exception in Start: {ex.Message}\n{ex.StackTrace}");
        }
    }

    void Update()
    {
        try
        {
            HandleUpdate();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Caught Exception in Update: {ex.Message}\n{ex.StackTrace}");
        }
    }

    void InitializeScene()
    {
        Debug.Log("Scene Initialized.");
    }

    void HandleUpdate()
    {
        // Add update logic if needed
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Exception || type == LogType.Error)
        {
            foreach (var suppressedError in suppressedErrors)
            {
                if (stackTrace.Contains(suppressedError))
                {
                    Debug.LogWarning($"[Suppressed Error] {logString} (Filtered to prevent crash)");
                    return; // Stop Unity from processing this error
                }
            }

            // Log other errors normally
            Debug.LogError($"[Global Error Handler] {logString}\n{stackTrace}");
        }
    }
}
