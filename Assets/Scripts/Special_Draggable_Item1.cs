using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Special_Draggable_Item : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] public int itemID;
    [SerializeField] public bool isPlaced;
    [SerializeField] public bool isCorrectlyPlaced;
    [SerializeField] public int itemSlotID;
    [SerializeField] private Local_Observer gameObserver;
    // [SerializeField] private DragnDrop_GameObserver gameObserver;  
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private float scaleFactor;
    public void Start()
    {
        originalPosition = rectTransform.anchoredPosition;
        Debug.Log("Everything should be working");
    }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;
        scaleFactor = canvas.scaleFactor;
    }
    public int GetItemID()
    {
        return itemID;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        SetCanvasGroupState(0.6f, false);
        isPlaced = false;
        Debug.Log("Canvas Scale Factor: " + canvas.scaleFactor);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / scaleFactor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Only reset the position if the item is placed and needs to be removed
        if (isPlaced)
        {
            if (isCorrectlyPlaced)
            {
                Debug.Log("Removing correctly placed item.");
                ResetPosition();
                isPlaced = false;
                isCorrectlyPlaced = false;
                itemSlotID = 99999;
                gameObserver.itemsPlaced--;
                gameObserver.correctlyPlacedItems--;
            }
            else
            {
                Debug.Log("Removing incorrectly placed item.");
                ResetPosition();
                isPlaced = false;
                itemSlotID = 99999;
                gameObserver.itemsPlaced--;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetCanvasGroupState(1f, true);

        // If the item is not placed in a valid slot, reset its position
        if (!isPlaced)
        {
            Debug.Log("Item not placed, returning to original position.");
            ResetPosition();
            isCorrectlyPlaced = false;
            itemSlotID = 99999;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    private void SetCanvasGroupState(float alpha, bool blocksRaycasts)
    {
        canvasGroup.alpha = alpha;
        canvasGroup.blocksRaycasts = blocksRaycasts;
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = originalPosition;
        SetCanvasGroupState(1f, true);
        isPlaced = false;
    }
}
