using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    public void SetSlider(float value) {
        slider.value = value;
    }

    public void SetMaxValueSlider(float value) {
        slider.maxValue = value;
        slider.value = value;
    }
}
