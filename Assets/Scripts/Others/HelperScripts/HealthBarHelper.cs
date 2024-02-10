using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarHelper : MonoBehaviour
{

    [SerializeField] private Slider healthSlider;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }
    void FixedUpdate()
    {
        if (healthSlider != null && text != null)
        {
            float currentValue = healthSlider.value;
            float maxValue = healthSlider.maxValue;
            text.SetText($"{currentValue}/{maxValue}");
        }
        else
        {
            Debug.LogWarning("Slider or TextMeshProUGUI components are not assigned.");
        }
    }
}
