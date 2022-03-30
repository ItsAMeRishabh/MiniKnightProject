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
    [SerializeField] private float yVelocity;

    public SpriteRenderer sp;
    private Animator anim;
    private Rigidbody2D rb;
    private PhotonView pView;

    public static CharacterContorller instanceController;

<<<<<<< Updated upstream
=======
    public GameObject InGameUI;
    public GameObject OverheadText;
    public GameObject groundSensor;
    public GameObject Aim;

>>>>>>> Stashed changes
    void Start()
    {
        instanceController = this;
        normalSpeed = walkSpeed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pView = GetComponent<PhotonView>();
        
    }

    void Update()
    {
        if(pView.IsMine)
        {
            Movement();
        }
    }

    public void Movement()
    {
        yVelocity  = rb.velocity.y;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * normalSpeed * Time.deltaTime);
            sp.flipX = true;
            anim.SetBool("isRunning", true);
        }

        if(Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * normalSpeed * Time.deltaTime);
            sp.flipX = false;
            anim.SetBool("isRunning", true);
        }

        if(Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("isRunning", false);
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && GroundCheck.instanceGroundCheck.isGrounded == true)
        {
            normalSpeed = sprintSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            normalSpeed = walkSpeed;
        }

        if(Input.GetMouseButton(1))
        {
            AudioManager.instance.PlayAudio("SwordSwing");
            anim.SetBool("isBlocking",true);
        }

        if(Input.GetMouseButtonUp(1))
        {
            anim.SetBool("isBlocking",false);
        }
        
        if(yVelocity < 0)
        {
            anim.SetBool("canJump",false);
            anim.SetBool("isFalling",true);
        }
        else if (yVelocity == 0)
        {
            anim.SetBool("isFalling",false);
        }

    }

    public void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
        GroundCheck.instanceGroundCheck.isGrounded = false;
        anim.SetBool("canJump", true);
    }
<<<<<<< Updated upstream
=======

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "WeaponPickup")
        {
            AimCharacter.aim_instance.GunDurability = 3;
            Destroy(other.gameObject);
            Aim.SetActive(true);
        }
    }
>>>>>>> Stashed changes
}