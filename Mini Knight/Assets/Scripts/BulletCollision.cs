using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public int bounce = 0;
    public float bulletSpeed = 10;
    
    void Update()
    {
        if(bounce == 3)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bounce++;
        
    }
}
