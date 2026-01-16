using UnityEngine;
using UnityEngine.Events;

public class GeneratorInteractable : Interactable
{
    [SerializeField] private AudioSource generatorAudio;
    [SerializeField] private UnityEvent onGeneratorStarted;
    [SerializeField] private UnityEvent onGeneratorStopped;

    private bool isRunning;

    protected override void OnInteract(GameObject interactor)
    {
        isRunning = !isRunning;
        if (generatorAudio != null)
        {
            if (isRunning)
            {
                generatorAudio.Play();
            }
            else
            {
                generatorAudio.Stop();
            }
        }

        if (isRunning)
        {
            onGeneratorStarted?.Invoke();
        }
        else
        {
            onGeneratorStopped?.Invoke();
        }
    }
}
