using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    [SerializeField]
    private Light Sun;
    [SerializeField]
    float secondsInNight = 120;

    [Range(0,1)][SerializeField]float currentTimeOfNight = 0;
    public float timeMultiplier = 1;
    float sunInitialIntensity;

    void Start()
    {
        sunInitialIntensity = Sun.intensity;
    }

    void Update()
    {
        UpdateSun();

        currentTimeOfNight += (Time.deltaTime / secondsInNight) * timeMultiplier;

        if(currentTimeOfNight >= 1)
        {
            currentTimeOfNight = 0;
        }
    }


    void UpdateSun()
    {
        Sun.transform.localRotation = Quaternion.Euler((currentTimeOfNight * 360f) - 90, 170, 0);

        float intensityMult = 1;
        
        if(currentTimeOfNight <= 0.23f ||  currentTimeOfNight > 0.75f)
        {
            intensityMult = 0;
        }
        else if ( currentTimeOfNight <= 0.25f)
        {
            intensityMult = Mathf.Clamp01((currentTimeOfNight - .23f) * (1 / .02f));
        }

        else if (currentTimeOfNight >= 0.73f)
        {
            intensityMult = Mathf.Clamp01(1 - ((currentTimeOfNight - .73f) * (1 / .02f)));
        }
        Sun.intensity = sunInitialIntensity * intensityMult;
    }
   
}
