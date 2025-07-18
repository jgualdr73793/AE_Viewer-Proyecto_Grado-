using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ModelManager : MonoBehaviour
{
    [Header("Model References")]
    public GameObject[] models;
    public Button leftButton;
    public Button rightButton;
    public TMP_Text modelCounterText;
    
    [Header("Dependencies")]
    public ObjectManipulator objectManipulator;
    public CameraController cameraController;
    
    [Header("Settings")]
    public float transitionSpeed = 0.5f;
    public bool enableSwipeNavigation = true;
    
    private int currentModelIndex = 0;
    private bool isTransitioning = false;

    void Start()
    {
        ValidateModels();
        InitializeModels();
        UpdateNavigationUI();
        
        leftButton.onClick.AddListener(ShowPreviousModel);
        rightButton.onClick.AddListener(ShowNextModel);
    }

    void ValidateModels()
    {
        if (models == null || models.Length == 0)
        {
            Debug.LogError("No models assigned in ModelManager!");
            return;
        }
    }

    void InitializeModels()
    {
        foreach (var model in models)
        {
            if (model != null) model.SetActive(false);
        }
        
        if (models[0] != null)
        {
            models[0].SetActive(true);
            SetCurrentModel(models[0].transform);
        }
    }

    void SetCurrentModel(Transform modelTransform)
    {
        objectManipulator.SetMainObject(modelTransform);
        StartCoroutine(TransitionToModelCamera(modelTransform));
    }

    public void ShowNextModel()
    {
        if (isTransitioning || currentModelIndex >= models.Length - 1) return;
        StartCoroutine(TransitionToModel(currentModelIndex + 1));
    }

    public void ShowPreviousModel()
    {
        if (isTransitioning || currentModelIndex <= 0) return;
        StartCoroutine(TransitionToModel(currentModelIndex - 1));
    }

    IEnumerator TransitionToModel(int newIndex)
    {
        isTransitioning = true;
        
        if (models[currentModelIndex] != null)
            models[currentModelIndex].SetActive(false);
        
        currentModelIndex = newIndex;
        
        if (models[currentModelIndex] != null)
        {
            models[currentModelIndex].SetActive(true);
            SetCurrentModel(models[currentModelIndex].transform);
        }
        
        UpdateNavigationUI();
        isTransitioning = false;
        yield return null;
    }

    void UpdateNavigationUI()
    {
        if (modelCounterText != null)
            modelCounterText.text = $"{currentModelIndex + 1}/{models.Length}";
        
        if (leftButton != null)
            leftButton.interactable = currentModelIndex > 0;
        
        if (rightButton != null)
            rightButton.interactable = currentModelIndex < models.Length - 1;
    }

    // Corrutina para mover cámara con transición suave al modelo
    public IEnumerator TransitionToModelCamera(Transform modelTransform)
    {
        if (isTransitioning) yield break;
        isTransitioning = true;

        cameraController.SetTarget(modelTransform);
        yield return cameraController.SmoothResetCameraPosition();

        isTransitioning = false;
    }
}
