using System;
using UnityEngine;
using UnityEngine.UI;

public class StressGradientHelper : MonoBehaviour
{
    public Gradient gradient;
    public Image stressFill;
    public Slider stressMeter;

    void FixedUpdate()
    {
        stressFill.color = gradient.Evaluate(stressMeter.normalizedValue);
    }
}
