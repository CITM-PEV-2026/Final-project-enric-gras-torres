using UnityEngine;
using UnityEngine.InputSystem;

public class staff : MonoBehaviour
{
    public float bulletSpeed;
    private MyActions controls;
    private CharacterController controller;
    private Vector3 direction;
    [SerializeField] private Transform cameraTransform;
    public GameObject bullet;

    private void Awake()
    {
        controls = new MyActions();
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        controls.CharacterController.Enable();
    }

    private void OnDisable()
    {
        controls.CharacterController.Disable();
    }


    private void Update()
    {
        if (controls.CharacterController.Cast.WasPerformedThisFrame())
        {
            CastSpell();
        }
    }

    private void CastSpell()
    {
        direction = cameraTransform.forward;

        Vector3 spawnPosition = cameraTransform.position + direction * 1f;

        GameObject newBullet = Instantiate(bullet, spawnPosition, cameraTransform.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.VelocityChange);

        Destroy(newBullet, 3f);
    }
}