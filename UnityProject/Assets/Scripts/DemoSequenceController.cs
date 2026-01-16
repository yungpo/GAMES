using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DemoSequenceController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SubtitleManager subtitleManager;
    [SerializeField] private ObjectiveUI objectiveUI;
    [SerializeField] private Light baseInteriorLight;
    [SerializeField] private AudioSource windAudio;
    [SerializeField] private AudioSource radioLoopAudio;

    [Header("Objective Markers")]
    [SerializeField] private GameObject generatorMarker;
    [SerializeField] private GameObject outpostMarker;

    [Header("Subtitle Lines")]
    [TextArea]
    [SerializeField] private string[] introLines =
    {
        "Ночь. Метель не утихает третий день.",
        "Я на дежурстве в отдалённой базе связи.",
        "Местные не любят этот лес. Говорят — он «слушает»."
    };

    [TextArea]
    [SerializeField] private string radioLine = "...не выходи...";

    [TextArea]
    [SerializeField] private string generatorObjective = "Проверить генератор у сарая.";

    [TextArea]
    [SerializeField] private string talismanObjective = "Вернуться в базу. Не трогать оберег.";

    [TextArea]
    [SerializeField] private string mapObjective = "Карта указывает старый пост связи в лесу.";

    [TextArea]
    [SerializeField] private string finaleLine = "Сигнал вернулся... но это не помеха.";

    [Header("Events")]
    [SerializeField] private UnityEvent onDemoComplete;

    private bool introPlayed;
    private bool radioTriggered;
    private bool generatorStarted;
    private bool talismanMoved;
    private bool mapRead;

    public void Configure(
        SubtitleManager newSubtitleManager,
        ObjectiveUI newObjectiveUI,
        Light newBaseInteriorLight,
        AudioSource newWindAudio,
        AudioSource newRadioLoopAudio,
        GameObject newGeneratorMarker,
        GameObject newOutpostMarker)
    {
        subtitleManager = newSubtitleManager;
        objectiveUI = newObjectiveUI;
        baseInteriorLight = newBaseInteriorLight;
        windAudio = newWindAudio;
        radioLoopAudio = newRadioLoopAudio;
        generatorMarker = newGeneratorMarker;
        outpostMarker = newOutpostMarker;
    }

    private void Start()
    {
        StartCoroutine(PlayIntro());
        if (generatorMarker != null)
        {
            generatorMarker.SetActive(false);
        }

        if (outpostMarker != null)
        {
            outpostMarker.SetActive(false);
        }

        if (windAudio != null)
        {
            windAudio.Play();
        }
    }

    private IEnumerator PlayIntro()
    {
        if (introPlayed || subtitleManager == null)
        {
            yield break;
        }

        introPlayed = true;
        foreach (string line in introLines)
        {
            subtitleManager.ShowLine(line, 3f);
            yield return new WaitForSeconds(3.5f);
        }

        if (objectiveUI != null)
        {
            objectiveUI.SetObjective("Проверить радио в доме.");
        }
    }

    public void HandleRadioUsed()
    {
        if (radioTriggered)
        {
            return;
        }

        radioTriggered = true;
        if (subtitleManager != null && !string.IsNullOrWhiteSpace(radioLine))
        {
            subtitleManager.ShowLine(radioLine, 3f);
        }

        if (generatorMarker != null)
        {
            generatorMarker.SetActive(true);
        }

        if (objectiveUI != null)
        {
            objectiveUI.SetObjective(generatorObjective);
        }
    }

    public void HandleGeneratorStarted()
    {
        if (generatorStarted)
        {
            return;
        }

        generatorStarted = true;
        if (objectiveUI != null)
        {
            objectiveUI.SetObjective(talismanObjective);
        }
    }

    public void HandleTalismanMoved()
    {
        if (talismanMoved)
        {
            return;
        }

        talismanMoved = true;
        if (baseInteriorLight != null)
        {
            baseInteriorLight.intensity *= 0.6f;
        }

        if (radioLoopAudio != null)
        {
            radioLoopAudio.Play();
        }

        if (objectiveUI != null)
        {
            objectiveUI.SetObjective(mapObjective);
        }
    }

    public void HandleMapRead()
    {
        if (mapRead)
        {
            return;
        }

        mapRead = true;
        if (outpostMarker != null)
        {
            outpostMarker.SetActive(true);
        }

        if (objectiveUI != null)
        {
            objectiveUI.SetObjective("Добраться до заброшенного поста связи.");
        }
    }

    public void HandleOutpostReached()
    {
        if (!mapRead)
        {
            return;
        }

        if (subtitleManager != null && !string.IsNullOrWhiteSpace(finaleLine))
        {
            subtitleManager.ShowLine(finaleLine, 4f);
        }

        if (objectiveUI != null)
        {
            objectiveUI.SetObjective("Демо завершено.");
        }

        onDemoComplete?.Invoke();
    }
}
