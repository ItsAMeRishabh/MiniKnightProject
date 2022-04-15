using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
  
    public float bulletSpeed = 10;
    
    void Update()
    {
        this.gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthBarScript.instance_health.currentHealth -= 10;
            //HealthBarScript.instance_health.SetHealth();
            Destroy(this.gameObject);
        }
    }
}