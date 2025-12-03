using System.Collections;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    [SerializeField] float onDuration;
    [SerializeField] float offDuration;
    private bool isOn = false;
    GameObject light;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource clockSource;

    
    void Start()
    {
        light = transform.GetChild(0).gameObject;
        StartCoroutine(blinkRoutine());
    }
    IEnumerator blinkRoutine()
    {
        if (isOn)
        {
            light.SetActive(true);
            if(anim != null)
            {
                anim.enabled = false;
            }
            if(clockSource != null)
            {
                clockSource.Pause();
            }
            yield return new WaitForSeconds(onDuration);
        }
        else
        {
            light.SetActive(false);
            if(anim != null)
            {
                anim.enabled = true;
            }
            if(clockSource != null)
            {
                clockSource.Play();
            }
            yield return new WaitForSeconds(offDuration);
        }
        isOn = !isOn;
        StartCoroutine(blinkRoutine());
    }
}
