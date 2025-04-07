using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitApplication : MonoBehaviour
{
    public void ExitApplication()
    {
        Debug.Log("Application exited");
        Application.Quit();
    }
}
