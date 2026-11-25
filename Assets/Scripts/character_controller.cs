using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class character_controller : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float minPitch = -85f;
    [SerializeField] private float maxPitch = 85f;
    [SerializeField] private bool lockCursor = true;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float pushForce = 2f;

    private MyActions controls;
    private CharacterController controller;
    private Vector2 moveInput;
    private float pitch;
    private float verticalVelocity;

    private void Awake()
    {
        controls = new MyActions();
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void OnEnable()
    {
        controls.CharacterController.Enable();
        controls.CharacterController.Movement.performed += OnMove;
        controls.CharacterController.Movement.canceled += OnMove;
    }

    private void OnDisable()
    {
        controls.CharacterController.Movement.performed -= OnMove;
        controls.CharacterController.Movement.canceled -= OnMove;
        controls.CharacterController.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        HandleLook();
        HandleMovement();
    }

    private void HandleLook()
    {
        if (Mouse.current == null || cameraTransform == null) return;

        Vector2 delta = Mouse.current.delta.ReadValue() * mouseSensitivity;

        // Horizontal rotation
        transform.Rotate(Vector3.up * delta.x);

        // Vertical rotation
        pitch -= delta.y;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // Press Escape to free the cursor
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void HandleMovement()
    {
        // Player's movement
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        movement = transform.TransformDirection(movement) * moveSpeed;

        // Gravity
        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += Physics.gravity.y * Time.deltaTime;
        movement.y = verticalVelocity;

        controller.Move(movement * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        if (rb == null || rb.isKinematic) return;

        if (hit.moveDirection.y < -0.3f) return;

        // Only push horizontally
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        rb.AddForce(pushDir * pushForce, ForceMode.Force);
    }
}