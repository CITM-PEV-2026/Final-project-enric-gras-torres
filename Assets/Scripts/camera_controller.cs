using UnityEngine;

public class camera_controller : MonoBehaviour
{
    public GameObject playerBody;
    public float sensitivity = 1.0f;
    private float xRotation = 1.0f;
    void Update()
    {
        // Read input from mouse or controller right stick
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Vertical look (camera)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Prevent flipping

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  // Apply vertical rotation

        // Horizontal look (player body)
        playerBody.transform.Rotate(Vector3.up * mouseX);
    }
}
