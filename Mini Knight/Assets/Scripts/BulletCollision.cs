using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public int bounce = 0;
    public float bulletSpeed;
    
    void Update()
    {
        if(bounce == 2)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bounce++;
        if(collision.gameObject.tag == "Player")
        {
            HealthBarScript.instance_health.currentHealth -= 20;
            HealthBarScript.instance_health.SetHealth();
        }
        
    }
}
