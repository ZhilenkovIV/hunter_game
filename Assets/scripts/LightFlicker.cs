using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float intensityMin;
    public float intensityMax;
    public float delay;
    private float currentTime = 0;
    private Light lightComponent;

    // Start is called before the first frame update
    void Start()
    {
        lightComponent = GetComponent<Light>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime < delay)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            lightComponent.intensity = intensityMin + Random.value * (intensityMax - intensityMin);
            currentTime = 0;
        }
    }
}
