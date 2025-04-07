using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{
    public GameObject targetObject;

    public GameObject toActivate;
    public GameObject toDeActivate;

    public void ToggleObject()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(!targetObject.activeSelf);
        }
        else
        {
            Debug.LogWarning("No target object assigned!");
        }
    }
    public void Switcheroo()
    {
        Debug.Log("Switcheroo");
        toActivate.SetActive(true);
        toDeActivate.SetActive(false);
    }
}
