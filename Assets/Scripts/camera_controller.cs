using UnityEngine;

/// <summary>
/// First-person mouse look.
/// Attach this to the Camera, which must be a child of the Player object.
/// Horizontal mouse movement rotates the player body (yaw).
/// Vertical mouse movement rotates only the camera (pitch).
/// </summary>
public class camera_controller : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float minPitch = -85f;
    [SerializeField] private float maxPitch = 85f;
    [SerializeField] private bool lockCursor = true;

    private Transform playerBody;
    private float pitch;

    private void Start()
    {
        // The camera's parent is the player
        playerBody = transform.parent;

        if (playerBody == null)
            Debug.LogError("FirstPersonCamera: the camera must be a child of the player.");

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        if (playerBody == null) return;

        // Legacy Input Manager. No Time.deltaTime here: mouse delta is already per-frame.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Yaw: rotate the whole player left/right
        playerBody.Rotate(Vector3.up * mouseX);

        // Pitch: rotate only the camera up/down, clamped so you can't flip over
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // Press Escape to free the cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}