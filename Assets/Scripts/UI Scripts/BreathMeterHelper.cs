using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathMeterHelper : MonoBehaviour
{
    CanvasInteractibilityScript cis;
    Slider breathSlider;

    void Start()
    {
        cis = GetComponent<CanvasInteractibilityScript>();
        breathSlider = GetComponent<Slider>();
    }

    void Update()
    {
        if(breathSlider.value > 0)
        {
            cis.ShowCanvas(false);
        } else
        {
            cis.HideCanvas(false);
        }
    }
}
