using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
public class ObjectDescription : MonoBehaviour
{
    [TextArea(3, 10)]
    public string description = "Ingrese la descripción aquí";

    [Header("Feedback Visual")]
    public Color highlightColor = Color.yellow;
    public float highlightIntensity = 1.5f;

    private Material originalMaterial;
    private Color originalColor;

    void Start()
    {
        if (GetComponent<Collider>() == null)
            gameObject.AddComponent<BoxCollider>();

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterial = renderer.material;
            originalColor = renderer.material.color;
        }
    }

    public void HighlightObject(bool highlight)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = highlight ? 
                highlightColor * highlightIntensity : 
                originalColor;
        }
    }
}
