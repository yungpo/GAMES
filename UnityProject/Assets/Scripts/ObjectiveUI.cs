using TMPro;
using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objectiveText;

    private void Awake()
    {
        if (objectiveText == null)
        {
            objectiveText = GetComponent<TextMeshProUGUI>();
        }
    }

    public void SetObjective(string text)
    {
        if (objectiveText == null)
        {
            return;
        }

        objectiveText.text = text;
        objectiveText.enabled = !string.IsNullOrWhiteSpace(text);
    }
}
