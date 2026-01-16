using UnityEngine;
using UnityEngine.Events;

public class TalismanInteractable : Interactable
{
    [SerializeField] private SubtitleManager subtitleManager;
    [TextArea]
    [SerializeField] private string subtitleLine = "Ветер усилился. Кажется, кто-то рядом.";
    [SerializeField] private float subtitleDuration = 3f;
    [SerializeField] private UnityEvent onTalismanMoved;

    private bool hasMoved;

    public void SetSubtitleManager(SubtitleManager manager)
    {
        subtitleManager = manager;
    }

    protected override void OnInteract(GameObject interactor)
    {
        if (hasMoved)
        {
            return;
        }

        hasMoved = true;
        if (subtitleManager != null && !string.IsNullOrWhiteSpace(subtitleLine))
        {
            subtitleManager.ShowLine(subtitleLine, subtitleDuration);
        }

        onTalismanMoved?.Invoke();
    }
}
