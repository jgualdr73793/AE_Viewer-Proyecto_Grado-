using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;
    public Vector3 targetOffset = new Vector3(0, 1f, 0);
    public float defaultDistance = 5f;
    public Vector3 defaultOffset = new Vector3(0, 2f, 0);

    [Header("Rotation Settings")]
    [SerializeField] private float rotateSpeed = 5f;

    [Header("Zoom Settings")]
    [SerializeField] private float zoomSpeed = 20f;
    [SerializeField] private float minZoomDistance = 2f;
    [SerializeField] private float maxZoomDistance = 15f;

    [Header("Transition Settings")]
    public float transitionDuration = 0.5f;

    void Update()
    {
        if (target == null) return;

        HandleRotation();
        HandleZoom();
    }

    void HandleRotation()
    {
        #if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetMouseButton(1))
        {
            float rotX = Input.GetAxis("Mouse X") * rotateSpeed;
            float rotY = Input.GetAxis("Mouse Y") * rotateSpeed;
            transform.RotateAround(target.position, Vector3.up, rotX);
            transform.RotateAround(target.position, transform.right, -rotY);
        }
        #endif
    }

    void HandleZoom()
    {
        #if UNITY_STANDALONE || UNITY_EDITOR
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            float currentDistance = Vector3.Distance(transform.position, target.position);
            float newDistance = Mathf.Clamp(currentDistance - scroll * zoomSpeed, minZoomDistance, maxZoomDistance);
            transform.position = target.position - transform.forward * newDistance;
        }
        #endif
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ResetCameraPosition()
    {
        if (target == null) return;

        transform.position = target.position - (target.forward * defaultDistance) + defaultOffset;
        transform.LookAt(target.position + targetOffset);
    }

    public IEnumerator SmoothResetCameraPosition()
    {
        if (target == null) yield break;

        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        Vector3 endPosition = target.position - (target.forward * defaultDistance) + defaultOffset;
        Quaternion endRotation = Quaternion.LookRotation(target.position + targetOffset - endPosition);

        float elapsed = 0f;
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / transitionDuration);

            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            yield return null;
        }

        transform.position = endPosition;
        transform.rotation = endRotation;
    }
}
