using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] private Animator heroAnimator;
    [SerializeField] private CharacterController2D controller;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float slopeCheckDistance;
    [SerializeField] private PhysicsMaterial2D noFriction;
    [SerializeField] private PhysicsMaterial2D withFriction;
    [SerializeField] private Transform groundCheck;


    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    private float horizontalMove = 0f;
    private float moveSpeed = 25f;
    private bool jump = false;
    private bool dodge = false;
    public bool crouch = false;
    public bool takingDamageByObstacle = false;
    private float slopeDownAngle;
    private int jumpCount = 0;

    private Vector2 colliderSize;

    private Vector3 horizontalMovement;

    private bool facingRight = true;
    private bool isGrounded = false;
    
    public bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        colliderSize = cc.size;
    }

    void Update()
    {
        if (controller.m_Grounded)
        {
            jumpCount = 0;
            heroAnimator.SetBool("Grounded", true);
        }
        else if (!controller.m_Grounded)
        {
            heroAnimator.SetBool("Grounded", false);
        }
        if(!controller.isDodging) slopeCheck();
        move();
        
    }

    private void FixedUpdate()
    {
        
        controller.Move(horizontalMove * Time.deltaTime, crouch, jump, dodge);
       
         jump = false;
         dodge = false;
         crouch = false;        
    }

    private void slopeCheck()
    {
        Vector2 checkPos = groundCheck.position;
        slopeCheckVertical(checkPos);
    }

    private void slopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, groundLayers);
        if(hit)
        {
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle > 0 || slopeDownAngle < 0) cc.sharedMaterial = withFriction;
            //else cc.sharedMaterial = noFriction;

            //Debug.DrawRay(hit.point, hit.normal, Color.red);
        }
    }

    void move()
    {               
            horizontalMove = Input.GetAxis("Horizontal") * moveSpeed;
            heroAnimator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            if (Input.GetKeyDown(KeyCode.LeftShift) && !controller.isDodging && !isAttacking && !takingDamageByObstacle)
            {
                heroAnimator.SetTrigger("Dodge");
                dodge = true;
            }
            if (Input.GetKeyDown(KeyCode.Space) && !controller.isDodging && jumpCount < 1)
            {
                heroAnimator.SetTrigger("Jump");
                jump = true;
                jumpCount++;
            }
            if (Input.GetKey (KeyCode.S) && !controller.isDodging && !isAttacking && !takingDamageByObstacle)
            {
                heroAnimator.SetBool("Crouch", true);
                //cc.sharedMaterial = withFriction;
                crouch = true;
            }
            else
            {
                heroAnimator.SetBool("Crouch", false);
                //cc.sharedMaterial = noFriction;
        }

            

        

    }
   
        ////////////////////////////////////////////////////////////////////////////
        
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            //if(collision.otherCollider.GetComponent<Enemy>().currentHealth <= 0)
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }
        if (collision.collider.tag == "ground")
        {
            isGrounded = true;
            
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground")
        {
            isGrounded = false;
            //heroAnimator.SetBool("Grounded", false);
        }
            
            
            
    }
}
