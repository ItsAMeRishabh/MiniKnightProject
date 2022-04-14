using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10;
    int bounceCount;

    private void Start()
    {
        bounceCount = 2;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            if (bounceCount > 0)
            {
                Vector3 reflectDir = Vector3.Reflect(transform.right, collision.contacts[0].normal);
                float rot = Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, 0, rot);
                bounceCount--;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        if (collision.transform.tag == "EdgeWall")
        {
            Destroy(gameObject);
        }
        if (collision.transform.tag == "FallDetector")
        {
            Destroy(gameObject);
        }
    }
}
