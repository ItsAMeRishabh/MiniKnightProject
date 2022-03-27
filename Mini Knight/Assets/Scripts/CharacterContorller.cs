using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterContorller : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float sprintSpeed = 4f;
    public float normalSpeed;
    public float jumpForce = 2f;
    public int extraJumps = 2;

    public PhotonView photonView;

    public SpriteRenderer sp;

    public static CharacterContorller instanceController;

    void Start()
    {
        instanceController = this;
        normalSpeed = walkSpeed;
        
    }

    void Update()
    {
        if(photonView.IsMine)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * normalSpeed * Time.deltaTime);
                sp.flipX = true;

            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * normalSpeed * Time.deltaTime);
                sp.flipX = false;

            }

            //JUMP
            if (Input.GetKeyDown(KeyCode.Space) && GroundCheck.instanceGroundCheck.isGrounded == true)
            {
                Jump();
            }

            //DOUBLE JUMP
            if (Input.GetKeyDown(KeyCode.Space) && GroundCheck.instanceGroundCheck.isGrounded == false)
            {
                if (extraJumps > 0)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
                    extraJumps--;
                }
            }

            //Sprinting
            if (Input.GetKeyDown(KeyCode.LeftShift) && GroundCheck.instanceGroundCheck.isGrounded == true)
            {
                normalSpeed = sprintSpeed;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                normalSpeed = walkSpeed;
            }
        }
    }

    public void Jump()
    {
        if (photonView.IsMine)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
            GroundCheck.instanceGroundCheck.isGrounded = false;
        }
    }
}