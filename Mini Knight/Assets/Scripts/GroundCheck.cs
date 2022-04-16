using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GroundCheck : MonoBehaviour
{
    public static GroundCheck instanceGroundCheck;

    public PhotonView pview;

    public bool isGrounded;
    public float lastGroundedTime;
    void Start()
    {
        instanceGroundCheck = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.tag == "Ground")
            {
                isGrounded = true;
                lastGroundedTime = 0;
            }
            else
            {
                isGrounded = false;
                lastGroundedTime -= Time.deltaTime;
            }
    }

}
