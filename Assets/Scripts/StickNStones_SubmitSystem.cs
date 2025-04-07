using UnityEngine;

public class StickNStones_SubmitSystem : MonoBehaviour
{
    public StickNStones_ZeroPair zeroPairChecker;
    public int correctAnswer;

    public void Submit()
    {
        zeroPairChecker.CheckZeroPairs();
        int sum = 0;
        foreach (Transform child in transform)
        {
            int value;
            if (int.TryParse(child.name, out value))
            {
                sum += value;
            }
        }
        if (sum == correctAnswer)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Wrong! Resetting...");
            // Reset logic here
        }
    }
}
