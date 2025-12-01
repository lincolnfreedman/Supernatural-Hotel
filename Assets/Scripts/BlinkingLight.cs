using System.Collections;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    [SerializeField] float onDuration;
    [SerializeField] float offDuration;
    private bool isOn = false;
    Light light;
    
    void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine(blinkRoutine());
    }
    IEnumerator blinkRoutine()
    {
        if (isOn)
        {
            light.enabled = true;
            yield return new WaitForSeconds(onDuration);
        }
        else
        {
            light.enabled = false;
            yield return new WaitForSeconds(offDuration);
        }
        isOn = !isOn;
        StartCoroutine(blinkRoutine());
    }
}
