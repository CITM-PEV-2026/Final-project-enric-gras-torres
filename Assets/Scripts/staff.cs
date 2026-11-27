using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class staff : MonoBehaviour
{
    private MyActions controls;
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
}
