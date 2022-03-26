using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GroundCheck : MonoBehaviour
{
    public static GroundCheck instanceGroundCheck;

    public GameObject groundCheck;

    public PhotonView pview;

    public bool isGrounded;
    void Start()
    {
        instanceGroundCheck = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
                isGrounded = true;
                CharacterContorller.instanceController.extraJumps = 2;
        }
        else
        {
            if (pview.IsMine)
            {
                isGrounded = false;
            }
        }

    }

}
