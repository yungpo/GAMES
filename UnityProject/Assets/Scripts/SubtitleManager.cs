using System.Collections;
using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI subtitleText;
    [SerializeField] private float defaultDuration = 3f;

    private Coroutine activeRoutine;

    public void SetText(TextMeshProUGUI text)
    {
        subtitleText = text;
    }

    public void ShowLine(string line)
    {
        ShowLine(line, defaultDuration);
    }

    public void ShowLine(string line, float duration)
    {
        if (subtitleText == null)
        {
            Debug.LogWarning("Subtitle text is not assigned.");
            return;
        }

        if (activeRoutine != null)
        {
            StopCoroutine(activeRoutine);
        }

        activeRoutine = StartCoroutine(ShowLineRoutine(line, duration));
    }

    private IEnumerator ShowLineRoutine(string line, float duration)
    {
        subtitleText.text = line;
        subtitleText.enabled = true;

        yield return new WaitForSeconds(duration);

        subtitleText.text = string.Empty;
        subtitleText.enabled = false;
        activeRoutine = null;
    }
}
