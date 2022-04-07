using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    Vector3 lastVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       // rb.AddForce(new Vector2(200f, 200f));
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        //lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            /* var speed = lastVelocity.magnitude;
             var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

             rb.velocity = direction * Mathf.Max(speed, 0f);*/
            Destroy(gameObject);
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
