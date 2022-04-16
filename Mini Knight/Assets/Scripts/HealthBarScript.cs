using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public static HealthBarScript instance_health;

    public int currentHealth;
    public int currentHearts;
    public float smoothing = 5f;

    public Slider sliderHealth;
    //public Slider OverheadSlider;

    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;

    public Vector3 offset;

    private void Start() 
    {
        instance_health = this;
        currentHealth = 100;
        sliderHealth.maxValue = currentHealth;
        sliderHealth.value = currentHealth;
        //OverheadSlider.maxValue = currentHealth;
        //OverheadSlider.value = currentHealth;
        
        currentHearts = 3;
    }

    /*public void SetHealth()
    {
        sliderHealth.value = currentHealth;
    }*/
    private void Update()
    {
        if(currentHearts>3)
        {
            currentHearts--;
        }

        if (sliderHealth.value != currentHealth)
        {
            sliderHealth.value = Mathf.Lerp(sliderHealth.value, currentHealth, smoothing * Time.deltaTime);
            //OverheadSlider.value = Mathf.Lerp(OverheadSlider.value, currentHealth, smoothing * Time.deltaTime);
        }

        //OverheadSlider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
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
