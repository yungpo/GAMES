using System;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactDistance = 2.5f;
    [SerializeField] private LayerMask interactMask = ~0;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    public event Action<string> PromptChanged;

    private IInteractable currentInteractable;

    private void Reset()
    {
        playerCamera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        UpdateFocus();
        HandleInteract();
    }

    private void UpdateFocus()
    {
        IInteractable nextInteractable = null;
        string nextPrompt = string.Empty;

        if (playerCamera != null)
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactMask))
            {
                nextInteractable = hit.collider.GetComponentInParent<IInteractable>();
                if (nextInteractable != null)
                {
                    nextPrompt = nextInteractable.Prompt;
                }
            }
        }

        if (nextInteractable != currentInteractable)
        {
            currentInteractable = nextInteractable;
            PromptChanged?.Invoke(nextPrompt);
        }
    }

    private void HandleInteract()
    {
        if (currentInteractable == null)
        {
            return;
        }

        if (Input.GetKeyDown(interactKey))
        {
            currentInteractable.Interact(gameObject);
        }
    }
}
