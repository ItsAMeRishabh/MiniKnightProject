using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider sliderHealth;

    public void MaxHealth(int health)
    {
        sliderHealth.maxValue = health;
        sliderHealth.value = health;
    }

    public void SetHealth(int health)
    {
        sliderHealth.value = health;
    }
}
