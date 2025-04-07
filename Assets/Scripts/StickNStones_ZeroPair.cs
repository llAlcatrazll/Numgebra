using System.Collections.Generic;
using UnityEngine;


public class StickNStones_ZeroPair : MonoBehaviour
{
    public List<GameObject> placedStones = new List<GameObject>();

    public void CheckZeroPairs()
    {
        for (int i = 0; i < placedStones.Count; i++)
        {
            for (int j = i + 1; j < placedStones.Count; j++)
            {
                if (placedStones[i].tag == "Positive" && placedStones[j].tag == "Negative")
                {
                    Destroy(placedStones[i]);
                    Destroy(placedStones[j]);
                    placedStones.RemoveAt(j);
                    placedStones.RemoveAt(i);
                    return;
                }
            }
        }
    }
}