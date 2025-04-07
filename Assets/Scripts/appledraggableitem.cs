using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class appledraggableitem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Local_Observer gameObserver;
    public Special_Item_Slot assignedSlot; // Reference to the slot this item was placed in
    [SerializeField] public bool isPlaced = false; // Track if item is placed

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Transform originalParent;
    private Vector2 dragOffset;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (assignedSlot != null) // If the item is placed in a slot, remove it first
        {
            assignedSlot.RemoveItem(this);
            assignedSlot = null;
            isPlaced = false;
            gameObserver.itemsPlaced--;
        }

        // Calculate offset to prevent large jumps
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out dragOffset);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(canvas.transform, true); // Bring to front
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            rectTransform.anchoredPosition = localPointerPosition - dragOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (assignedSlot == null) // If not placed in a slot, return to original position
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        StartCoroutine(MoveBackToPosition());
        assignedSlot = null;
        isPlaced = false;
    }

    private IEnumerator MoveBackToPosition()
    {
        float duration = 0.2f;
        float elapsedTime = 0f;
        Vector2 startPosition = rectTransform.anchoredPosition;

        while (elapsedTime < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, originalPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = originalPosition;
    }
}
