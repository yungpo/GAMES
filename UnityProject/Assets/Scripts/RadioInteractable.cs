using UnityEngine;

public class RadioInteractable : Interactable
{
    [SerializeField] private AudioSource radioAudio;
    [SerializeField] private SubtitleManager subtitleManager;
    [TextArea]
    [SerializeField] private string subtitleLine = "...не выходи...";
    [SerializeField] private float subtitleDuration = 3f;

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
    }
}
