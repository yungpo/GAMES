using UnityEngine;
using UnityEngine.Events;

public class MapInteractable : Interactable
{
    [SerializeField] private SubtitleManager subtitleManager;
    [TextArea]
    [SerializeField] private string subtitleLine = "На карте отмечен старый пост связи.";
    [SerializeField] private float subtitleDuration = 3f;
    [SerializeField] private UnityEvent onMapRead;

    public void SetSubtitleManager(SubtitleManager manager)
    {
        subtitleManager = manager;
    }

    protected override void OnInteract(GameObject interactor)
    {
        if (subtitleManager != null && !string.IsNullOrWhiteSpace(subtitleLine))
        {
            subtitleManager.ShowLine(subtitleLine, subtitleDuration);
        }

        onMapRead?.Invoke();
    }
}
