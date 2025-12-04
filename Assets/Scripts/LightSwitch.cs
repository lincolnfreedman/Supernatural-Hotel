using UnityEngine;
using TMPro;

public class LightSwitch : Interactable
{
    [SerializeField] GameObject lightContainer;
    [SerializeField] GameObject interactPrompt;
    [SerializeField] AudioClip switchSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactPrompt.SetActive(true);
            interactPrompt.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "Press E to flip the light switch";
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
        if (!PlayerController.instance.powerOff)
        {
            lightContainer.SetActive(!lightContainer.activeSelf);       
        }
        AudioSource.PlayClipAtPoint(switchSound, transform.position);
    }
}
