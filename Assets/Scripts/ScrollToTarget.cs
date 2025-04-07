using UnityEngine;
using UnityEngine.UI;

public class ScrollToTarget : MonoBehaviour
{
    public ScrollRect scrollRect;  // Assign the ScrollRect
    public RectTransform content;  // The content inside the ScrollRect
    public RectTransform objectOffset; // The offset object (child of content)

    public void AlignContentToScrollRect()
    {
        // Get ScrollRect's position
        Vector2 scrollRectPos = scrollRect.GetComponent<RectTransform>().anchoredPosition;

        // Apply offset by adding objectOffset's anchoredPosition
        content.anchoredPosition = scrollRectPos - objectOffset.anchoredPosition;
    }
}
