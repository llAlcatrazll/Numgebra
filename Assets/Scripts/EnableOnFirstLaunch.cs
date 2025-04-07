using UnityEngine;

public class EnableOnFirstLaunch : MonoBehaviour
{
    public GameObject targetObject; // Assign in Inspector

    private void Start()
    {
        if (!PlayerPrefs.HasKey("FirstLaunch"))
        {
            // First time launching the app
            PlayerPrefs.SetInt("FirstLaunch", 1);
            PlayerPrefs.Save(); // Save the preference

            if (targetObject != null)
            {
                targetObject.SetActive(true);
                Debug.Log("Target object enabled on first launch.");
            }
        }
        else
        {
            // Not the first launch, make sure it's disabled
            if (targetObject != null)
            {
                targetObject.SetActive(false);
                Debug.Log("Target object disabled on subsequent launches.");
            }
        }
    }
}
