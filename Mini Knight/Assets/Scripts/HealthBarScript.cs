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

    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;

    public Vector3 offset;

    public GameObject pickupHeart;

    private void Start() 
    {
        instance_health = this;
        currentHealth = 100;
        sliderHealth.maxValue = currentHealth;
        sliderHealth.value = currentHealth;
        
        currentHearts = 3;
    }

    private void Update()
    {
        if(currentHearts>3)
        {
            currentHearts--;
        }

        if (sliderHealth.value != currentHealth)
        {
            sliderHealth.value = Mathf.Lerp(sliderHealth.value, currentHealth, smoothing * Time.deltaTime);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "HeartPNG")
        {
            if (currentHearts < 3)
            {
                Destroy(collision.gameObject);
                currentHearts++;
            }
        }
    }
}
