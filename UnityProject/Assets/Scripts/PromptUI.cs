using TMPro;
using UnityEngine;

public class PromptUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;

    private void Awake()
    {
        if (promptText == null)
        {
            promptText = GetComponent<TextMeshProUGUI>();
        }
    }

    public void SetPrompt(string prompt)
    {
        if (promptText == null)
        {
            return;
        }

        promptText.text = prompt;
        promptText.enabled = !string.IsNullOrWhiteSpace(prompt);
    }
}
