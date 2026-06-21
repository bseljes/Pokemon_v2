using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private bool showDebugRay = true;
    [SerializeField] public int isInTallGrass = 0;
    // [SerializeField] private int stepsToEncounter;

    private void Awake()
    {
        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();
        }
    }

    private void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        if (showDebugRay && playerCamera != null)
        {
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactDistance, Color.yellow);
        }

        if (GameManager.Instance != null && !GameManager.Instance.IsGameplay)
        {
            return;
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (TextBubbleUI.Instance != null && (TextBubbleUI.Instance.IsShowing || TextBubbleUI.Instance.WasClosedThisFrame))
            {
                return;
            }
 
            TryInteract();
        }
    }

    private void TryInteract()
    {
        if (playerCamera == null)
        {
            Debug.LogWarning("PlayerInteractor needs a camera reference.");
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (!Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            Debug.Log("No interactable object in range.");
            return;
        }

        if (hit.collider.TryGetComponent(out Interactable interactable))
        {
            interactable.Interact(gameObject);
            return;
        }

        Debug.Log($"Looked at {hit.collider.name}, but it does not have an Interactable script.");
    }
}
