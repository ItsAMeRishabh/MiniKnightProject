using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public static HealthBarScript instance_health;
    public Slider sliderHealth;
    public int currentHealth;


    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;


    public int currentHearts;

    private void Start() 
    {
        instance_health = this;
        currentHealth = 100;
        sliderHealth.maxValue = currentHealth;

        currentHearts = 3;
    }

    public void SetHealth()
    {
        sliderHealth.value = currentHealth;
    }

    public void UpdateHearts()
    {
        switch (currentHearts)
        {
            case 3:
                Heart1.SetActive(true);
                Heart2.SetActive(true);
                Heart3.SetActive(true);
                break;
            case 2:
                Heart1.SetActive(true);
                Heart2.SetActive(true);
                Heart3.SetActive(false);
                break;
            case 1:
                Heart1.SetActive(true);
                Heart2.SetActive(false);
                Heart3.SetActive(false);
                break;
        }
    }

    
}
