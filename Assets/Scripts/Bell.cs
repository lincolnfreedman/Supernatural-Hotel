using UnityEngine;
using TMPro;


public class Bell : Interactable
{
    [SerializeField] GameObject interactPrompt;
    [SerializeField] AudioClip bellSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactPrompt.SetActive(true);
            interactPrompt.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "Press E to ring the bell";
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
        AudioSource.PlayClipAtPoint(bellSound, transform.position);
    }
}
