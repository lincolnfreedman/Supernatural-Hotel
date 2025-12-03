using UnityEngine;
using TMPro;

public class Door : Interactable
{
    [SerializeField] GameObject interactPrompt;
    [SerializeField] GameObject endText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactPrompt.SetActive(true);
            if(PlayerController.instance.hasKey)
            {
                interactPrompt.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "Press E to open the door";
            }
            else
            {
                interactPrompt.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "The door is locked";
            }
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
        if(PlayerController.instance.hasKey)
        {
            PlayerController.instance.FreezePlayer();
            endText.SetActive(true);
        }
    }
}
