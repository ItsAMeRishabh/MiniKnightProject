using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float offset;

    public GameObject bullet;

    public Transform firePoint;

    private float timeBtwShots;
    public float startTimeBtwShots;

  /*  private void Start()
    {
        timeBtwShots = 0;
    }*/

    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        /*if(timeBtwShots<=0)
        {*/

            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                //timeBtwShots = startTimeBtwShots;
                
            }
            /*else
            {
                timeBtwShots = Time.deltaTime;
            }
        }*/
        
    }
}
