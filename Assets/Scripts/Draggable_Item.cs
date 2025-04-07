using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable_Item : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] public int itemID;
    [SerializeField] public bool isPlaced;
    [SerializeField] public bool isCorrectlyPlaced;
    [SerializeField] public int itemSlotID;
    [SerializeField] private Local_Observer gameObserver;


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
        if (isPlaced)
        {
            ResetPlacedItem();
            return; // Prevent dragging if item is being reset
        }
        // Calculate offset to avoid large cursor jumps
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out dragOffset);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        originalPosition = rectTransform.anchoredPosition;
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
        transform.SetParent(originalParent, true);

        if (!isPlaced)
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        StartCoroutine(MoveBackToPosition());

        isPlaced = false;
        isCorrectlyPlaced = false;
        itemSlotID = 99999;
    }

    private IEnumerator MoveBackToPosition()
    {
        float duration = 0.2f; // Duration of the movement
        float elapsedTime = 0f;
        Vector2 startPosition = rectTransform.anchoredPosition;

        while (elapsedTime < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, originalPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        rectTransform.anchoredPosition = originalPosition; // Ensure exact position at the end
    }
    public void ResetPlacedItem()
    {
        if (isCorrectlyPlaced)
        {
            gameObserver.correctlyPlacedItems--;
        }
        ResetPosition();

        // Update game observer values


        gameObserver.itemsPlaced--;


        // Reset item state
        isPlaced = false;
        isCorrectlyPlaced = false;
        itemSlotID = 99999;
    }

}