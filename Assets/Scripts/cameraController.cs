using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 8f;
    public Vector3 offset;

    [Header("Camera Settings")]
    public float minFOV = 15f;   // Minimum field of view for scrolling
    public float maxFOV = 90f;   // Maximum field of view for scrolling
    public float scrollSensitivity = 10f; // Sensitivity of scroll for zooming
    public float zoomSpeed = 2f;  // Speed for smooth transitions
    public float rightClickZoomFOV = 35f; // The specific FOV when right-clicking to zoom in

    private Camera cam;
    private float targetFOV;  // The current target FOV, controlled by scroll

    void Start()
    {
        cam = GetComponent<Camera>();  // Get the camera component attached to this object
        targetFOV = cam.fieldOfView;   // Initialize targetFOV with the current camera FOV
    }

    void Update()
    {
        if (target == null) return;

        // Smooth follow the target
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Adjust camera FOV with mouse scroll wheel
        AdjustFOV();

        // Adjust camera FOV with right mouse button (specific zoom)
        RightClickZoom();
    }

    void AdjustFOV()
    {
        // Get scroll input from the mouse
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            // Adjust the target FOV based on scroll input
            targetFOV -= scrollInput * scrollSensitivity;
            targetFOV = Mathf.Clamp(targetFOV, minFOV, maxFOV);  // Clamp FOV between min and max
        }

        // Set the camera FOV to smoothly move towards the targetFOV
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }

    void RightClickZoom()
    {
        if (Input.GetMouseButton(1)) // Right mouse button is pressed
        {
            // Zoom in directly to the specific FOV (35)
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, rightClickZoomFOV, Time.deltaTime * zoomSpeed);
        }
        else
        {
            // When the right mouse button is released, return to the target FOV (last scrolled position)
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
        }
    }
}
