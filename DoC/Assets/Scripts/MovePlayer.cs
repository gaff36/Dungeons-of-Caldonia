using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private float moveSpeed = 2f;
    private float jumpForce = 3f;
    private float dashForce = 2.75f;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] BoxCollider2D standHead;
    [SerializeField] BoxCollider2D standBody;
    [SerializeField] BoxCollider2D crouchBody;
    private Vector3 movement;
    private bool facingRight = true;
    private bool isGrounded = false;
    private bool isDashing = false;

    private DateTime start;
    private DateTime end;
                

    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);    
        Flip();
        Jump();
        Dash();
        StopDash();
        HeavyAttack();
        StopHeavyAttack();
        playerAnimator.SetFloat("Speed", Mathf.Abs(movement.x));
        transform.position += movement * Time.deltaTime * moveSpeed;      
    }

    void Flip()
    {
        if (movement.x > 0)
        {
            facingRight = true;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        else if (movement.x < 0)
        {
            facingRight = false;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && !isDashing)
        {
            playerAnimator.SetBool("Jump", true);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);       
        }      
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && facingRight && !isDashing && isGrounded)
        {
            start = DateTime.Now;
            playerAnimator.SetBool("Dash", true);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dashForce, 0f), ForceMode2D.Impulse);
            isDashing = true;
            standHead.enabled = false;
            standBody.enabled = false;
            crouchBody.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && !facingRight && !isDashing && isGrounded)
        {
            start = DateTime.Now;
            playerAnimator.SetBool("Dash", true);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * dashForce, 0f), ForceMode2D.Impulse);
            isDashing = true;
            standHead.enabled = false;
            standBody.enabled = false;
            crouchBody.enabled = true;
        }        
    }

    void StopDash()
    {
        if (isDashing)
        {
            end = DateTime.Now;
            double elapsedTime = (end - start).TotalSeconds;
            if (elapsedTime > 0.5d)
            {
                isDashing = false;
                playerAnimator.SetBool("Dash", false);
                standHead.enabled = true;
                standBody.enabled = true;
                crouchBody.enabled = false;
            }
        }
    }

    void HeavyAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isDashing)
        {
            start = DateTime.Now;
            playerAnimator.SetBool("HeavyAttack", true);
        }
    }

    void StopHeavyAttack()
    {
        end = DateTime.Now;
        double elapsedTime = (end - start).TotalSeconds;
        if (elapsedTime > 0.7d)
        {
            playerAnimator.SetBool("HeavyAttack", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "ground")
        isGrounded = true;
        playerAnimator.SetBool("Jump", false);
        playerAnimator.SetBool("Grounded", true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground")
        isGrounded = false;
        playerAnimator.SetBool("Grounded", false);
    }

}

