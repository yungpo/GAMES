using UnityEngine;

public class DoorInteractable : Interactable
{
    [SerializeField] private Transform doorTransform;
    [SerializeField] private Vector3 openRotation = new Vector3(0f, 90f, 0f);
    [SerializeField] private float openSpeed = 3f;

    private bool isOpen;
    private Quaternion closedRotation;
    private Quaternion targetRotation;

    private void Awake()
    {
        if (doorTransform == null)
        {
            doorTransform = transform;
        }

        closedRotation = doorTransform.localRotation;
        targetRotation = closedRotation;
    }

    private void Update()
    {
        doorTransform.localRotation = Quaternion.Lerp(doorTransform.localRotation, targetRotation, Time.deltaTime * openSpeed);
    }

    protected override void OnInteract(GameObject interactor)
    {
        isOpen = !isOpen;
        targetRotation = isOpen
            ? closedRotation * Quaternion.Euler(openRotation)
            : closedRotation;
    }
}
