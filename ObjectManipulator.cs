using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectManipulator : MonoBehaviour
{
    [Header("Main References")]
    public Transform mainObject;
    public CameraController cameraController; 
    public TMP_Text descriptionText;
    public Canvas backgroundCanvas;
    public ModelManager modelManager;

    [Header("Selection Settings")]
    [SerializeField] private LayerMask selectableLayers = ~0;
    [SerializeField] private float doubleClickTime = 0.3f;

    public Transform selectedObject { get; private set; }
    private float lastClickTime;

    private List<GameObject> allObjects = new List<GameObject>();

    private Dictionary<Transform, Vector3> originalPositions = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, Quaternion> originalRotations = new Dictionary<Transform, Quaternion>();

    private bool isObjectCentered = false;
    private int outsideClickCount = 0;

    void Start()
    {
        InitializeObjectList();
        DisableBackgroundRaycasting();
    }

    void InitializeObjectList()
    {
        allObjects.Clear();
        originalPositions.Clear();
        originalRotations.Clear();

        if (mainObject != null)
        {
            allObjects.Add(mainObject.gameObject);
            StoreOriginalTransform(mainObject);

            foreach (Transform child in mainObject)
            {
                if (child != null)
                {
                    allObjects.Add(child.gameObject);
                    StoreOriginalTransform(child);
                }
            }
        }
    }

    public void SetMainObject(Transform newMainObject)
    {
        mainObject = newMainObject;
        InitializeObjectList();
    }

    void Update()
    {
        HandleSelection();
    }

    void HandleSelection()
    {
        bool inputDetected = false;
        Ray ray = default;

        #if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            inputDetected = true;
        }
        #endif

        #if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            inputDetected = true;
        }
        #endif

        if (!inputDetected) return;

        DisableBackgroundRaycasting();

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, selectableLayers))
        {
            if (hit.transform == null)
            {
                HandleOutsideClick();
                return;
            }

            if (Time.time - lastClickTime < doubleClickTime)
            {
                CenterObject(hit.transform);
                isObjectCentered = true;
                outsideClickCount = 0;
            }

            lastClickTime = Time.time;
        }
        else
        {
            HandleOutsideClick();
        }

        EnableBackgroundRaycasting();
    }

    void HandleOutsideClick()
    {
        outsideClickCount++;

        if (outsideClickCount >= 2)
        {
            RestoreAllObjects();
            isObjectCentered = false;
            outsideClickCount = 0;
        }
    }

    public void CenterObject(Transform obj)
    {
        if (obj == null) return;

        selectedObject = obj;
        obj.position = Vector3.zero;

        if (cameraController != null)
        {
            cameraController.SetTarget(obj);
            StartCoroutine(cameraController.SmoothResetCameraPosition());
        }

        ShowDescription(obj);
    }

    void ShowDescription(Transform obj)
    {
        if (obj == null || descriptionText == null) return;

        ObjectDescription desc = obj.GetComponent<ObjectDescription>();
        if (desc != null)
        {
            descriptionText.text = desc.description;
            descriptionText.gameObject.SetActive(true);
        }
        else
        {
            descriptionText.gameObject.SetActive(false);
        }
    }

    void StoreOriginalTransform(Transform obj)
    {
        if (obj == null) return;

        if (!originalPositions.ContainsKey(obj))
        {
            originalPositions[obj] = obj.position;
            originalRotations[obj] = obj.rotation;
        }
    }

    void RestoreOriginalTransform(Transform obj)
    {
        if (obj == null) return;

        if (originalPositions.TryGetValue(obj, out Vector3 savedPos))
        {
            obj.position = savedPos;
        }

        if (originalRotations.TryGetValue(obj, out Quaternion savedRot))
        {
            obj.rotation = savedRot;
        }
    }

    void RestoreAllObjects()
    {
        foreach (var obj in originalPositions.Keys)
        {
            RestoreOriginalTransform(obj);
        }

        if (modelManager != null && mainObject != null)
        {
            StartCoroutine(modelManager.TransitionToModelCamera(mainObject));
        }

        if (descriptionText != null)
        {
            descriptionText.gameObject.SetActive(false);
        }

        selectedObject = null;
    }

    void DisableBackgroundRaycasting()
    {
        if (backgroundCanvas != null)
        {
            backgroundCanvas.GetComponent<GraphicRaycaster>().enabled = false;
            Image bgImage = backgroundCanvas.GetComponentInChildren<Image>();
            if (bgImage != null) bgImage.raycastTarget = false;
        }
    }

    void EnableBackgroundRaycasting()
    {
        if (backgroundCanvas != null)
        {
            backgroundCanvas.GetComponent<GraphicRaycaster>().enabled = true;
            Image bgImage = backgroundCanvas.GetComponentInChildren<Image>();
            if (bgImage != null) bgImage.raycastTarget = true;
        }
    }

    public void ResetView()
    {
        RestoreAllObjects();
        isObjectCentered = false;
        outsideClickCount = 0;
    }
}
