using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt = "Interact";

    public virtual string Prompt => prompt;

    public void Interact(GameObject interactor)
    {
        OnInteract(interactor);
    }

    protected abstract void OnInteract(GameObject interactor);
}
