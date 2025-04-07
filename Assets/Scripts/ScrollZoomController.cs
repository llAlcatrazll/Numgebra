using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollZoomController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ScrollRect scrollRect;      // The ScrollRect component
    public RectTransform content;      // The content inside ScrollRect
    public float minZoom = 0.5f;       // Minimum zoom scale
    public float maxZoom = 3f;         // Maximum zoom scale
    public float zoomSpeed = 0.1f;     // Speed of zooming (for touch)
    public float mouseZoomSpeed = 0.5f; // Speed of zooming (for mouse)

    private bool isZooming = false;
    private Vector2 lastTouch0, lastTouch1;

    void Start()
    {
        GetComponent<CanvasRenderer>().cullTransparentMesh = true;

        if (content != null)
        {
            content.localScale = new Vector3(minZoom, minZoom, 1f);
        }
    }

    void Update()
    {
        HandleTouchZoom();
        HandleMouseZoom();
    }

    void HandleTouchZoom()
    {
        if (Input.touchCount == 2) // Detects multi-touch (pinch zoom)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (!isZooming)
            {
                lastTouch0 = touch0.position;
                lastTouch1 = touch1.position;
                isZooming = true;
            }
            else
            {
                float prevDistance = Vector2.Distance(lastTouch0, lastTouch1);
                float currentDistance = Vector2.Distance(touch0.position, touch1.position);
                float zoomFactor = (currentDistance - prevDistance) * zoomSpeed;

                Zoom(zoomFactor);

                lastTouch0 = touch0.position;
                lastTouch1 = touch1.position;
            }
        }
        else
        {
            isZooming = false;
        }
    }

    void HandleMouseZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel"); // Get mouse scroll input
        if (scroll != 0)
        {
            Zoom(scroll * mouseZoomSpeed);
        }
    }

    void Zoom(float zoomFactor)
    {
        float newScale = Mathf.Clamp(content.localScale.x + zoomFactor, minZoom, maxZoom);
        content.localScale = new Vector3(newScale, newScale, 1f);
        AdjustScrollPosition();
    }

    void AdjustScrollPosition()
    {
        // Keeps content inside bounds after zooming
        float scaledWidth = content.rect.width * content.localScale.x;
        float scaledHeight = content.rect.height * content.localScale.y;

        float xMin = -scaledWidth * 0.5f;
        float xMax = scaledWidth * 0.5f;
        float yMin = -scaledHeight * 0.5f;
        float yMax = scaledHeight * 0.5f;

        scrollRect.content.anchoredPosition = new Vector2(
            Mathf.Clamp(scrollRect.content.anchoredPosition.x, xMin, xMax),
            Mathf.Clamp(scrollRect.content.anchoredPosition.y, yMin, yMax)
        );
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        scrollRect.StopMovement(); // Stop movement when touching
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        scrollRect.StopMovement();
    }
}
