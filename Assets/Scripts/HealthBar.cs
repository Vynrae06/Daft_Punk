using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider sliderFill;

    private void Start()
    {
        sliderFill = GetComponent<Slider>();
    }

    public void SetHealth(int health)
    {
        sliderFill.value = health;
    }
}
