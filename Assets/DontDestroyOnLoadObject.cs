using UnityEngine;

public class DontDestroyOnLoadObject : MonoBehaviour
{
    // This will automatically apply DontDestroyOnLoad when the object is instantiated
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
