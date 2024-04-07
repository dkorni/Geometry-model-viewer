using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    public float zoomSpeed = 1.0f;
    public float minZoomDistance = 2.0f;
    public float maxZoomDistance = 10.0f;
    public Transform target;
    public Camera camera;

    private Vector3 lastMousePosition;

    void Update()
    {
        // Rotation
        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            transform.RotateAround(target.position, Vector3.forward, -delta.x * rotationSpeed);
            transform.RotateAround(target.position, transform.right, delta.y * rotationSpeed);
        }

        // Zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            float fieldOfView = scroll * zoomSpeed;
            camera.fieldOfView = Mathf.Clamp(camera.fieldOfView + 1 * -fieldOfView, minZoomDistance, maxZoomDistance);
        }

        lastMousePosition = Input.mousePosition;
    }
}
