using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TouchRotationStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Stick References")]
    public RectTransform stickBackground;
    public RectTransform stickHandle;

    [Header("Rotation Settings")]
    public float stickRange = 50f;
    public float rotationSpeed = 2f;
    public float returnSpeed = 5f;

    [Header("Visual Feedback")]
    public Color activeColor = new Color(1, 1, 1, 0.8f);
    public Color inactiveColor = new Color(1, 1, 1, 0.4f);

    private Vector2 inputVector = Vector2.zero;
    private bool isDragging = false;
    private Image handleImage;
    private Image backgroundImage;
    private CameraController cameraController;

    void Start()
    {
        if (stickBackground == null) stickBackground = GetComponent<RectTransform>();
        if (stickHandle == null && stickBackground.childCount > 0)
            stickHandle = stickBackground.GetChild(0).GetComponent<RectTransform>();

        handleImage = stickHandle.GetComponent<Image>();
        backgroundImage = stickBackground.GetComponent<Image>();
        cameraController = FindObjectOfType<CameraController>();

        SetStickVisuals(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            stickBackground, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            Vector2 clamped = Vector2.ClampMagnitude(localPoint, stickRange);
            stickHandle.localPosition = clamped;
            inputVector = clamped / stickRange;

            if (!isDragging)
            {
                isDragging = true;
                SetStickVisuals(true);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        stickHandle.localPosition = Vector3.zero;

        if (isDragging)
        {
            isDragging = false;
            SetStickVisuals(false);
        }
    }

    void Update()
    {
        if (!isDragging || cameraController == null || cameraController.target == null) return;

        float rotX = inputVector.x * rotationSpeed * Time.deltaTime * 60f;
        float rotY = inputVector.y * rotationSpeed * Time.deltaTime * 60f;

        cameraController.transform.RotateAround(cameraController.target.position, Vector3.up, rotX);
        cameraController.transform.RotateAround(cameraController.target.position, cameraController.transform.right, -rotY);

        if (inputVector == Vector2.zero)
        {
            stickHandle.localPosition = Vector3.Lerp(stickHandle.localPosition, Vector3.zero, returnSpeed * Time.deltaTime);
        }
    }

    private void SetStickVisuals(bool active)
    {
        if (handleImage != null)
            handleImage.color = active ? activeColor : inactiveColor;

        if (backgroundImage != null)
            backgroundImage.color = active ? activeColor : inactiveColor;
    }

    // ✅ Ya no ocultamos el joystick según plataforma
    void OnEnable()
    {
        stickBackground.gameObject.SetActive(true);
    }
}
