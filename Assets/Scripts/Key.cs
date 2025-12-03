using UnityEngine;
using TMPro;

public class Key : Interactable
{
    [SerializeField] GameObject interactPrompt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactPrompt.SetActive(true);
            interactPrompt.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "Press E to pick up the key";
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
        PlayerController.instance.currentInteractable = null;
        Destroy(gameObject);
        interactPrompt.SetActive(false);
        PlayerController.instance.hasKey = true;
    }
}
