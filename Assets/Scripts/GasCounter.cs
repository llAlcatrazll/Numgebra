using UnityEngine;
using TMPro;

public class GasCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text GasCount;
    [SerializeField] private LevelLoader levelLoader; // Reference to LevelLoader

    void Start()
    {
        UpdateGasCount();
    }

    void UpdateGasCount()
    {
        if (levelLoader != null)
        {
            int totalGasCans = levelLoader.GetTotalGasCans();
            GasCount.text = $"{totalGasCans} / 100 Gasoline";
        }
        else
        {
            Debug.LogError("[ERROR] LevelLoader reference is missing!");
        }
    }
}
