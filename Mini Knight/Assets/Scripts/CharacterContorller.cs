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
    public Animator anim;
    private Rigidbody2D rb;
    public PhotonView pView;

    public static CharacterContorller instanceController;

    public GameObject InGameUI;
    public GameObject OverheadText;
    public GameObject TextPlayer;
    public GameObject groundSensor;
    public GameObject aimStick;
    public GameObject playerPrefab;

    void Start()
    {
        instanceController = this;
        normalSpeed = walkSpeed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pView = GetComponent<PhotonView>();
        
        if(!pView.IsMine)
        {
            InGameUI.SetActive(false);
            OverheadText.SetActive(false);
            groundSensor.SetActive(false);

            playerPrefab.layer = LayerMask.NameToLayer("Enemy");
        }
            aimStick.SetActive(false);
    }

    void Update()
    {
        if(pView.IsMine)
        {
            Movement();

            RangedShoot();
            
            if (Input.GetMouseButtonDown(0))
            {       
                anim.SetBool("isAttacking1", true);
            }
            else
            {
                anim.SetBool("isAttacking1", false);
            }
        }

    }

    public void Movement()
    {
            yVelocity = rb.velocity.y;

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.right * normalSpeed * Time.deltaTime);
                //sp.flipX = true;
                anim.SetBool("isRunning", true);
                transform.eulerAngles = new Vector3(0, 180, 0);
                TextPlayer.transform.eulerAngles = new Vector3(0, 0, 0);
        }
            
            if (Input.GetKeyUp(KeyCode.A))
            {
            
                anim.SetBool("isRunning", false);
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * normalSpeed * Time.deltaTime);
                //sp.flipX = false;
                anim.SetBool("isRunning", true);
                transform.eulerAngles = new Vector3(0,0, 0);
                TextPlayer.transform.eulerAngles = new Vector3(0, 0, 0);
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                anim.SetBool("isRunning", false);
            }

            //JUMP
            if (Input.GetKeyDown(KeyCode.Space) && GroundCheck.instanceGroundCheck.isGrounded == true)
            {
                Jump();
            }

            if(GroundCheck.instanceGroundCheck.isGrounded == true)
            {
                extraJumps = 2;
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

           /* if (Input.GetMouseButton(1))
            {
                AudioManager.instance.PlayAudio("SwordSwing");
                anim.SetBool("isBlocking", true);
            }

            if (Input.GetMouseButtonUp(1))
            {
                anim.SetBool("isBlocking", false);
            }*/

            if (yVelocity < 0)
            {
                anim.SetBool("canJump", false);
                anim.SetBool("isFalling", true);
            }
            else if (yVelocity == 0)
            {
                anim.SetBool("isFalling", false);
            }
    }
    
    public void RangedShoot()
    {
        //Aim Stick enabling on Right Mouse Click.
        if (Input.GetMouseButton(1))
        {
            aimStick.SetActive(true);
        }
        else
        {
            aimStick.SetActive(false);
        }
    }
    
   

    public void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
        GroundCheck.instanceGroundCheck.isGrounded = false;
        anim.SetBool("canJump", true);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "WeaponPickup")
        {
            Shooting.instanceShooting.BulletCount += 3;
            Spawn_Powerups.instance.SowrdCount--;
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Heart")
        {
            HealthBarScript.instance_health.currentHearts++;
            HealthBarScript.instance_health.UpdateHearts();
            Spawn_Powerups.instance.HealthCount--;
            Destroy(other.gameObject);
        }
    }
}