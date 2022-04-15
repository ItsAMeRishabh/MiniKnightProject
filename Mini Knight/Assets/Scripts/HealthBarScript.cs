using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public static HealthBarScript instance_health;

    public int currentHealth;
    public int currentHearts;

    public Slider sliderHealth;

    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;


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
    private void Update()
    {
        if(currentHearts>3)
        {
            currentHearts--;
        }
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

    public void GiveDamage()
    {
        
    }
}
