using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorController : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public float movementInputDirection;
    public float verticalMovementInputDirection;
    private Animator animator;
    [SerializeField] private Animator smokeAnimator;
    [SerializeField] private FixedJoystick joystick;

    //

    public bool onAir;
    public bool touchingLadder;
    public bool climbingLadder;
    public bool isOnLedge;
    public bool onTopOfLadder;
    public bool onJumpMotion;
    public float jumpHangTime;
    public float horizontalLadderPosition;
    public bool done;

    //

    public float dashSpeed;
    private float lastDashTime;

    //

    private float lastJumpTime;
    public float ladderPosition;

    //

    private bool crouching;

    //

    [SerializeField] private float damageTime;
    [SerializeField] private GameObject noSign;


    //


    private int dashingDirection;
    public bool isFacingRight = true;
    private bool walking;
    public bool grounded;
    public bool jumpButtonPressed;
    public bool jumpButtonTriggered;
    public bool dodgeButtonTriggered;
    private bool canJump;
    public bool canMove = true;
    private int facingDirection;
    public bool dashing = false;
    private bool nextDashAvailable = true;
    private bool canFlip = true;
    private bool move;
    [SerializeField] private bool isOnSlope;
    [SerializeField] private float ledgeCheckDistance;


    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

    public int amountOfJumpsLeft;

    [SerializeField] private WarriorCombatController warriorCombatController;
    
    [SerializeField] private Transform groundCheck1;
    [SerializeField] private Transform groundCheck2;
    [SerializeField] private Transform groundCheck3;
    [SerializeField] private Transform ledgeCheckPoint;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsStairs;
    [SerializeField] private LayerMask whatIsLadder;
    [SerializeField] private float groundCheckRange;
    private float groundCheck3Range = 0.35f;
    [SerializeField] public float movementSpeed;
    [SerializeField] private float climbSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [SerializeField] private int maxJumpAmount;

    public PhysicsMaterial2D friction;
    public PhysicsMaterial2D frictionless;
    public PhysicsMaterial2D stairsFrictionHeavy;
    public PhysicsMaterial2D stairsFrictionLight;

    [SerializeField] private BoxCollider2D bc1;
    [SerializeField] private BoxCollider2D bc2;
    [SerializeField] private CircleCollider2D cc;

    //

    [SerializeField] private AudioSource jumpSound2;
    [SerializeField] private AudioSource jumpSound1;
    [SerializeField] private AudioSource dodgeSound;
    [SerializeField] private AudioSource footStepSound;

    private float lastMovementTime;
    private float movementSoundCooldown = 0.3f;

    //

    void Start()
    {
        done = false;
        canMove = true;
        facingDirection = 1;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        amountOfJumpsLeft = maxJumpAmount;
    }

    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        checkIfCanJump();
        updateAnimations();
        slopeCheckVertical();
    }

    private void FixedUpdate()
    {
        if (warriorCombatController.isDead)
        {
            bc1.enabled = false;
            bc2.enabled = false;
            cc.enabled = true;
            canMove = false;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            if ((!grounded || isOnSlope) && warriorCombatController.takingDamage) amountOfJumpsLeft = 0;
            if (!warriorCombatController.isAttacking) EnableFlip();
            ApplyMovement();
            CheckSurroundings();
            jumpButtonPressed = false;

            if (done) rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if (Time.time >= lastDashTime + dashCooldown || (lastDashTime == 0 && Time.time < dashCooldown))
        {
            noSign.SetActive(false);
        }
        else noSign.SetActive(true);

        }

    ////////////////////////////////////////////////////////////////

    private void CheckSurroundings()
    {
        bool check = false;

        if (!grounded) check = true;
        
        if (grounded)
        {
            grounded = Physics2D.OverlapCircle(groundCheck1.position, groundCheckRange, whatIsGround)
                       || Physics2D.OverlapCircle(groundCheck2.position, groundCheckRange, whatIsGround)
                       || Physics2D.OverlapCircle(groundCheck2.position, groundCheckRange, whatIsGround);
        }
        else
        {
            grounded = Physics2D.OverlapCircle(groundCheck3.position, groundCheckRange, whatIsGround);
        }

        if (grounded && check && !isOnSlope)
        {
            smokeAnimator.SetTrigger("landed");
        }

        isOnLedge = (Physics2D.Raycast(ledgeCheckPoint.position, Vector2.down, ledgeCheckDistance, whatIsGround));
    }

    private void updateAnimations()
    {
        animator.SetBool("move", move);
        animator.SetBool("grounded", grounded || isOnSlope);
        animator.SetBool("dashing", dashing);
        animator.SetBool("crouch", crouching);
        animator.SetBool("climbingLadder", climbingLadder);
        animator.SetFloat("verticalVelocity", rb.velocity.y);
        animator.SetFloat("verticalVelocityAbs", Mathf.Abs(rb.velocity.y));
        animator.SetBool("hurt", warriorCombatController.takingDamage);

    }
    private void CheckMovementDirection()
    {
        if (isFacingRight && movementInputDirection < 0) Flip();
        else if (!isFacingRight && movementInputDirection > 0) Flip();
    }

    private void Flip()
    {
        if (canFlip)
        {
            isFacingRight = !isFacingRight;
            if (isFacingRight) facingDirection = 1;
            else facingDirection = -1;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    public void DisableFlip()
    {
        canFlip = false;
    }

    public void EnableFlip()
    {
        canFlip = true;
    }

    private void CheckInput()
    {
        if (canMove && !warriorCombatController.isAttacking && !done) 
        {
            if (joystick.Vertical > 0.6f && joystick.Vertical <= 1f) verticalMovementInputDirection = 1f;
            else if (joystick.Vertical < -0.6f && joystick.Vertical >= -1f) verticalMovementInputDirection = -1f;
            else verticalMovementInputDirection = 0f;
        }
        if (canMove && !done)
        {
            if(joystick.Horizontal > 0.25f && joystick.Horizontal <= 1f) movementInputDirection = 1f;
            else if(joystick.Horizontal < -0.25f && joystick.Horizontal >= -1f) movementInputDirection = -1f;
            else movementInputDirection = 0f;

            if (movementInputDirection == 0) move = false;
            else move = true;
        }

        else move = false;
        if (jumpButtonTriggered && !crouching)
        {
            jumpButtonTriggered = false;

            if (dashing) finishDash();
            if(warriorCombatController.isAttacking)
            {
                warriorCombatController.FinishAttack1();
                EnableFlip();
            }
            jumpButtonPressed = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            Jump();
        }
        if (dodgeButtonTriggered)
        {
            dodgeButtonTriggered = false;

            if (warriorCombatController.isAttacking && grounded && Time.time >= lastDashTime + dashCooldown)
            {
                EnableFlip();
                warriorCombatController.FinishAttack1();
            }
            if(!dashing && grounded && !isOnSlope && !climbingLadder && !warriorCombatController.isAttacking)
            {
                startDash();
            }
        }

        if(joystick.Vertical <= -0.75f && !dashing && !climbingLadder && grounded && !warriorCombatController.isAttacking && !warriorCombatController.isDead)
        {
            startCrouch();
        }
        else if (joystick.Vertical > -0.75f && !dashing && grounded && !warriorCombatController.isDead)
        {
            finishCrouch();
        }
    }

    private void ApplyMovement()
    {
        if (warriorCombatController.isAttacking && grounded && isOnLedge && !warriorCombatController.takingDamage)
        {
            if (dashing) finishDash();
            if(crouching) finishCrouch();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            Vector3 targetVelocity = new Vector2(facingDirection * movementSpeed / 3f, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
        else if(warriorCombatController.isAttacking && grounded && !isOnLedge && !warriorCombatController.takingDamage)
        {
            if (dashing) finishDash();
            if (crouching) finishCrouch();
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else if (climbingLadder && warriorCombatController.isAttacking && !warriorCombatController.takingDamage)
        {
            Debug.Log("FELL");
            climbingLadder = false;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else if (canMove && !warriorCombatController.isAttacking && !warriorCombatController.takingDamage)
        {       

            if(Time.time <= lastJumpTime + jumpHangTime)
            {
                onJumpMotion = true;
            }
            else
            {
                onJumpMotion = false;
            }
         
            if(climbingLadder && warriorCombatController.takingDamage)
            {
                Debug.Log("FELL");
                climbingLadder = false;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            
            else if(jumpButtonPressed)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                climbingLadder = false;
            }
            else if ((climbingLadder && verticalMovementInputDirection == 0 && !onJumpMotion) 
                      || (climbingLadder && verticalMovementInputDirection == 1 && onTopOfLadder && !onJumpMotion))
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
            else if(climbingLadder && verticalMovementInputDirection != 0)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                //climbingLadder = true;
                rb.velocity = new Vector2(0f, verticalMovementInputDirection * climbSpeed);
            }
            else if(onTopOfLadder && !onJumpMotion && verticalMovementInputDirection != 0 && !warriorCombatController.isDead)
            {

                //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                //rb.velocity = new Vector2(0f, verticalMovementInputDirection * climbSpeed);
                rb.velocity = new Vector2(0f, 0f);
                transform.position = new Vector2(horizontalLadderPosition, transform.position.y);
                climbingLadder = true;
            }



            if (!climbingLadder && !dashing && !crouching)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                Vector3 targetVelocity = new Vector2(movementInputDirection * movementSpeed, rb.velocity.y);
                rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
                if (!done && (grounded || isOnSlope) && movementInputDirection != 0 && Time.time >= lastMovementTime + movementSoundCooldown)
                {
                    lastMovementTime = Time.time;
                    footStepSound.Play();
                }
            }

            if(dashing)
            {
                if(dashingDirection != facingDirection)
                {
                    finishDash();
                }
                else if(Time.time >= lastDashTime + dashTime)
                {
                    finishDash();
                }
                else if(jumpButtonPressed)
                {
                    finishDash();
                }
            }

        }
    }

    private void startDash()
    {
        if (Time.time >= lastDashTime + dashCooldown || (lastDashTime == 0 && Time.time < dashCooldown))
        {
            dodgeSound.Play();
            lastDashTime = Time.time;
            dashing = true;
            dashingDirection = facingDirection;
            bc1.enabled = false;

            bc2.sharedMaterial = frictionless;

            rb.velocity = new Vector2(dashingDirection * dashSpeed, rb.velocity.y);
        }     
        
    }

    private void finishDash()
    {
        bc1.sharedMaterial = friction;
        bc1.enabled = true;

        if(!warriorCombatController.isAttacking) rb.velocity = new Vector2(0f, rb.velocity.y);
        dashing = false;
    }

        private void Jump()
        {
        if (canJump)
        {
            
            lastJumpTime = Time.time;
            rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Force);
            
            amountOfJumpsLeft--;
            if (amountOfJumpsLeft == 0 && !climbingLadder && !grounded)
            {
                jumpSound2.Play();
                smokeAnimator.SetTrigger("secondJump");
            }
            else jumpSound1.Play();
        }
        }

    private void checkIfCanJump()
    {
        if ((grounded ) || climbingLadder || isOnSlope)
        {
            amountOfJumpsLeft = maxJumpAmount;
        }
        if (amountOfJumpsLeft > 0) canJump = true;
        else canJump = false;
    }

    private void slopeCheckVertical()
    {
        isOnSlope = Physics2D.OverlapCircle(groundCheck2.position, groundCheckRange, whatIsStairs);
        //if (isOnSlope) cc.sharedMaterial = frictionless;
        //else if(!dashing) cc.sharedMaterial = friction;


        if (onJumpMotion)
        {
            bc1.sharedMaterial = frictionless;
        }
        else if (isOnSlope && movementInputDirection == 0 && !dashing)
        {
            bc1.sharedMaterial = stairsFrictionHeavy;
        }
        else if (isOnSlope && movementInputDirection == 1)
        {
            bc1.sharedMaterial = stairsFrictionLight;
        }
        else if (isOnSlope && movementInputDirection == -1)
        {
            bc1.sharedMaterial = stairsFrictionLight;

        }
        else if (grounded && !dashing)
        {
            bc1.sharedMaterial = friction;
        }
        else
        {
            bc1.sharedMaterial = frictionless;
        }
    }

    private void startCrouch()
    {
        crouching = true;

        Vector3 targetVelocity = new Vector2(0f,0f);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        bc1.enabled = false;
    }

    private void finishCrouch()
    {
        crouching = false;
        canMove = true;
        bc1.enabled = true;
    }

    public void damageHop(float horizontalDamageDirection)
    {

            int direction;
            if (horizontalDamageDirection > transform.position.x) direction = -1;
            else direction = 1;

            if(climbingLadder) rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            rb.velocity = new Vector2(0f, 0f);
            rb.AddForce(new Vector2(100f * direction, 100f));

        
    }

    ////////////////////////////////////////////////////////////////
  
    public void pressJumpButton()
    {
        jumpButtonTriggered = true;
    }

    public void pressDodgeButton()
    {
        dodgeButtonTriggered = true;
    }

    ////////////////////////////////////////////////////////////////


    /*
    private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(groundCheck1.position, groundCheckRange);
            Gizmos.DrawWireSphere(groundCheck2.position, groundCheckRange);
            Gizmos.DrawWireSphere(groundCheck3.position, groundCheckRange);

        }           */
}
