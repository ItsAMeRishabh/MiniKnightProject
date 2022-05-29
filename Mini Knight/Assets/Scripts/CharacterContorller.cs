using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CharacterContorller : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float normalSpeed;
    public float jumpForce = 2f;
    public int extraJumps = 2;

    //Variables for Dashing
    public float dashSpeed;
    public float dashLength = 0.5f;
    public float dashCooldown = 1f;
    private float dashCounter;
    public float dashCoolCounter;

    //For Force Based Movement
    public float moveInput;
    [SerializeField] private float accel;
    [SerializeField] private float deccel;
    [SerializeField] private float velPow;
    [SerializeField] private float frictionAmount;
    [SerializeField] private float gravityScale;
    [SerializeField] private float fallGravityMultiplier;
    [SerializeField] private float yVelocity;

    public SpriteRenderer sp;
    public Animator anim;
    private Rigidbody2D rb;
    public PhotonView pView;

    public static CharacterContorller instanceController;

    public GameObject InGameUI;
    public GameObject OverheadText;
    //public GameObject OverheadHealthBarUI;
    public GameObject TextPlayer;
    public GameObject groundSensor;
    public GameObject aimStick;
    public GameObject playerPrefab;

    public ParticleSystem dashParticle;
    public ParticleSystem dustParticle;

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
            JumpMovement();

            DashBoost();
            
            RangedShoot();

            ChangeDir();

            //attack animation trigger
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetBool("isAttacking1", true);
                //anim.SetBool("isRunning", false);
                //anim.SetBool("isFalling", false);
            }
            else
            {
                anim.SetBool("isAttacking1", false);
            }
        }
    }

    public void JumpMovement()
    {
            yVelocity = rb.velocity.y;
            moveInput = Input.GetAxis("Horizontal");

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
                    rb.velocity = Vector2.zero;
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    extraJumps--;
                    anim.SetTrigger("isJumping");
                }
            }

            if(GroundCheck.instanceGroundCheck.isGrounded == true)
            {
                extraJumps = 2;
            }

        #region animtriggers
        if (yVelocity < 0)
            {
                anim.SetBool("isFalling", true);
                rb.gravityScale = gravityScale * fallGravityMultiplier;
            }
            else if (yVelocity == 0)
            {
                anim.SetBool("isFalling", false);
            }
            if (yVelocity != 0)
            {
                anim.SetBool("isRunning", false);
            }
        #endregion

        //For Jumping calculations
        if (yVelocity >0)
            {
                rb.gravityScale = gravityScale;
            }
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        GroundCheck.instanceGroundCheck.isGrounded = false;
        anim.SetTrigger("isJumping");
        playDust();
    }

    private void FixedUpdate()
    {
        //Force Based Movement
        float targetSpeed = moveInput * normalSpeed;

        float speedDif = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accel : deccel;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPow) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);

        //Friction
        if(GroundCheck.instanceGroundCheck.lastGroundedTime>0 && Mathf.Abs(moveInput)<0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x),Mathf.Abs(frictionAmount));

            amount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }

        if(moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            TextPlayer.transform.eulerAngles = new Vector3(0, 0, 0);
            anim.SetBool("isRunning", true);
        }

        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            TextPlayer.transform.eulerAngles = new Vector3(0, 0, 0);
            anim.SetBool("isRunning", true);
        }

        if(moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void ChangeDir()
    {
        if(moveInput > 0 && Input.GetKeyDown(KeyCode.A) && GroundCheck.instanceGroundCheck.isGrounded)
        {
            playDust();
        }
        if (moveInput < 0 && Input.GetKeyDown(KeyCode.D) && GroundCheck.instanceGroundCheck.isGrounded)
        {
            playDust();
        }
    }

    public void DashBoost()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(dashCoolCounter==1 && dashCounter<=0)
            {
                playDash();
                normalSpeed = dashSpeed;
                dashCounter = dashLength;
                dashCoolCounter = 0;
            }
        }

        if(dashCounter>0)
        {
            dashCounter -= Time.deltaTime;

            if(dashCounter <= 0)
            {
                normalSpeed = walkSpeed;
            }
        }

        if(GroundCheck.instanceGroundCheck.isGrounded)
        {
            dashCoolCounter= 1;
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
    
    public void playDash()
    {
        dashParticle.Play();
    }

    public void playDust()
    {
        dustParticle.Play();
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



#region BackupCode

//Backup Movement
/*if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.right * normalSpeed * Time.deltaTime);
                //sp.flipX = true;
                
                
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
                
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                anim.SetBool("isRunning", false);
            }*/

//BLOCKING
/* if (Input.GetMouseButton(1))
         {
             AudioManager.instance.PlayAudio("SwordSwing");
             anim.SetBool("isBlocking", true);
         }

         if (Input.GetMouseButtonUp(1))
         {
             anim.SetBool("isBlocking", false);
         }*/

#endregion