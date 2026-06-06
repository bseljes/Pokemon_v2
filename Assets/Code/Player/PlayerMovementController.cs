using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float sprintSpeed = 7f;
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravity = -20f;

    [Header("Mouse Look")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float mouseSensitivity = 0.12f;
    [SerializeField] private float minLookAngle = -75f;
    [SerializeField] private float maxLookAngle = 75f;
    [SerializeField] private bool lockCursorOnStart = true;

    [Header("Debug")]
    [SerializeField] private bool logJumpDebug;
    [SerializeField] private bool isGrounded;
    [SerializeField] private string lastControllerHit;

    private CharacterController characterController;
    private Vector3 verticalVelocity;
    private float cameraPitch;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Start()
    {
        if (lockCursorOnStart)
        {
            LockCursor();
        }
    }

    private void Update()
    {
        if (Keyboard.current == null || Mouse.current == null)
        {
            return;
        }

        HandleCursor();

        if (GameManager.Instance != null && !GameManager.Instance.IsGameplay)
        {
            return;
        }

        HandleLook();
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 input = Vector2.zero;
        Keyboard keyboard = Keyboard.current;
        isGrounded = characterController.isGrounded;

        if (keyboard.wKey.isPressed)
        {
            input.y += 1f;
        }

        if (keyboard.sKey.isPressed)
        {
            input.y -= 1f;
        }

        if (keyboard.dKey.isPressed)
        {
            input.x += 1f;
        }

        if (keyboard.aKey.isPressed)
        {
            input.x -= 1f;
        }

        input = Vector2.ClampMagnitude(input, 1f);

        float speed = keyboard.leftShiftKey.isPressed ? sprintSpeed : walkSpeed;
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        characterController.Move(move * speed * Time.deltaTime);

        if (isGrounded && verticalVelocity.y < 0f)
        {
            verticalVelocity.y = -2f;
        }

        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            if (logJumpDebug)
            {
                Debug.Log($"Space pressed. isGrounded: {isGrounded}, last hit: {lastControllerHit}");
            }

            if (isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        characterController.Move(verticalVelocity * Time.deltaTime);
        isGrounded = characterController.isGrounded;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        lastControllerHit = hit.collider.name;
    }

    private void HandleLook()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            return;
        }

        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseDelta.x);

        if (cameraTransform == null)
        {
            return;
        }

        cameraPitch = Mathf.Clamp(cameraPitch - mouseDelta.y, minLookAngle, maxLookAngle);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    private void HandleCursor()
    {
        Keyboard keyboard = Keyboard.current;
        Mouse mouse = Mouse.current;

        if (keyboard.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (mouse.leftButton.wasPressedThisFrame && Cursor.lockState != CursorLockMode.Locked)
        {
            LockCursor();
        }
    }

    private static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
