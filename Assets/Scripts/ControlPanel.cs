using TMPro;
using UnityEngine;

public class ControlPanel : Interactable
{
    [SerializeField] GameObject interactPrompt;
    [SerializeField] AudioClip switchSound;
    [SerializeField] AudioClip screechSound;
    private int pressCount = 0;
    private string text = "Press E to flip the reception switch";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactPrompt.SetActive(true);
            interactPrompt.GetComponentsInChildren<TextMeshProUGUI>()[0].text = text;
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
        switch (pressCount)
        {
            case 0:
                foreach(FreezeLight light in PlayerController.instance.freezeLights)
                {
                    light.gameObject.SetActive(false);
                }
                BlinkingLight[] blinkingLights = FindObjectsByType<BlinkingLight>(FindObjectsSortMode.None);
                foreach(BlinkingLight blink in blinkingLights)
                {
                    blink.EndBlinking();
                }
                PlayerController.instance.powerOff = true;
                AudioSource.PlayClipAtPoint(switchSound, transform.position);
                pressCount++;
                text = "Press E to flip the kitchen switch";
                interactPrompt.GetComponentsInChildren<TextMeshProUGUI>()[0].text = text;
                break;
            case 1:
                pressCount++;
                AudioSource.PlayClipAtPoint(switchSound, transform.position);
                text = "Press E to flip the floor 2 switch";
                interactPrompt.GetComponentsInChildren<TextMeshProUGUI>()[0].text = text;
                break;
            case 2:
                pressCount++;
                AudioSource.PlayClipAtPoint(switchSound, transform.position);
                text = "Press E to flip the basement switch";
                interactPrompt.GetComponentsInChildren<TextMeshProUGUI>()[0].text = text;
                break;
            case 3:
                AudioSource.PlayClipAtPoint(switchSound, transform.position);
                AudioSource.PlayClipAtPoint(screechSound, transform.position);
                text = "Nothing left to press";
                interactPrompt.GetComponentsInChildren<TextMeshProUGUI>()[0].text = text;
                break;
        }
    }
}
