using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float offset;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public int BulletCount;

    public GameObject bullet;
    //public GameObject Aimm;

    public Transform firePoint;
    public Animator anim;

    public static Shooting instanceShooting;

    private void Start()
    {
        BulletCount = 3;
        instanceShooting = this;
    }
    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (BulletCount >= 1)
        {
            if (timeBtwShots <= 0)
            {
                if (Input.GetMouseButtonDown(0))
                {            
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    timeBtwShots = startTimeBtwShots;
                    BulletCount--;
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }
}
