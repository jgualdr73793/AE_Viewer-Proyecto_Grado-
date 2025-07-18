using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TouchZoomSlider : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Slider References")]
    public RectTransform sliderBackground;
    public RectTransform sliderHandle;

    [Header("Zoom Settings")]
    public float zoomRange = 100f; // Máxima distancia que se puede mover el slider
    public float zoomSensitivity = 0.05f; // Cuánto afecta cada unidad a la distancia de la cámara
    public float minZoomDistance = 2f;
    public float maxZoomDistance = 15f;

    private CameraController cameraController;
    private Vector2 inputStart;
    private float initialZoomDistance;

    void Start()
    {
        if (sliderBackground == null) sliderBackground = GetComponent<RectTransform>();
        if (sliderHandle == null && sliderBackground.childCount > 0)
            sliderHandle = sliderBackground.GetChild(0).GetComponent<RectTransform>();

        cameraController = FindObjectOfType<CameraController>();

        if (cameraController == null)
            Debug.LogWarning("No CameraController found in scene.");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(sliderBackground, eventData.position, eventData.pressEventCamera, out inputStart);
        initialZoomDistance = GetCurrentCameraDistance();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (cameraController == null || cameraController.target == null) return;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(sliderBackground, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
        {
            float deltaY = Mathf.Clamp(localPoint.y - inputStart.y, -zoomRange, zoomRange);

            // Mover la manija visualmente
            sliderHandle.localPosition = new Vector2(sliderHandle.localPosition.x, deltaY);

            // Calcular zoom
            float zoomAmount = -deltaY * zoomSensitivity;
            float newDistance = Mathf.Clamp(initialZoomDistance + zoomAmount, minZoomDistance, maxZoomDistance);

            // Aplicar nueva posición a la cámara
            Vector3 dir = (cameraController.transform.position - cameraController.target.position).normalized;
            cameraController.transform.position = cameraController.target.position + dir * newDistance;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Reset visual
        sliderHandle.localPosition = Vector3.zero;
    }

    private float GetCurrentCameraDistance()
    {
        if (cameraController == null || cameraController.target == null) return 0f;
        return Vector3.Distance(cameraController.transform.position, cameraController.target.position);
    }
}
