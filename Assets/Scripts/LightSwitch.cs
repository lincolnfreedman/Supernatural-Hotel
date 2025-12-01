using UnityEngine;

public class LightSwitch : Interactable
{
    [SerializeField] GameObject lightContainer;
    [SerializeField] GameObject interactPrompt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactPrompt.SetActive(true);
            PlayerController.instance.currentInteractable = this;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactPrompt.SetActive(false);
            if(PlayerController.instance.currentInteractable == this)
            {
                PlayerController.instance.currentInteractable = null;
            }
        }
    }
    public override void Interact()
    {
        lightContainer.SetActive(!lightContainer.activeSelf);
    }
}
