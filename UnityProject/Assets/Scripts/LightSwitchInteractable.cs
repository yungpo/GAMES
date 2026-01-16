using UnityEngine;

public class LightSwitchInteractable : Interactable
{
    [SerializeField] private Light targetLight;
    [SerializeField] private AudioSource toggleAudio;

    private void Awake()
    {
        if (targetLight == null)
        {
            targetLight = GetComponentInChildren<Light>();
        }
    }

    protected override void OnInteract(GameObject interactor)
    {
        if (targetLight == null)
        {
            return;
        }

        targetLight.enabled = !targetLight.enabled;
        if (toggleAudio != null)
        {
            toggleAudio.Play();
        }
    }
}
