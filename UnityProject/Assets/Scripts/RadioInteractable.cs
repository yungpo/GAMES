using UnityEngine;
using UnityEngine.Events;

public class RadioInteractable : Interactable
{
    [SerializeField] private AudioSource radioAudio;
    [SerializeField] private SubtitleManager subtitleManager;
    [TextArea]
    [SerializeField] private string subtitleLine = "...не выходи...";
    [SerializeField] private float subtitleDuration = 3f;
    [SerializeField] private UnityEvent onRadioUsed;

    public void SetSubtitleManager(SubtitleManager manager)
    {
        subtitleManager = manager;
    }

    protected override void OnInteract(GameObject interactor)
    {
        if (radioAudio != null)
        {
            radioAudio.Play();
        }

        if (subtitleManager != null && !string.IsNullOrWhiteSpace(subtitleLine))
        {
            subtitleManager.ShowLine(subtitleLine, subtitleDuration);
        }

        onRadioUsed?.Invoke();
    }
}
