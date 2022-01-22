using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum State
    {
        walking, 
        takingDamage, 
        dead
    }

    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject alive;
    private State currentState;
    private bool groundDetected;
    private bool wallDetected;
    private int facingDirection;
    private int damageDirection;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float knockbackStartTime;
    private Animator aliveAnimator;

    [SerializeField] private float knockbackDuration;
    [SerializeField] private Vector2 knockbackSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float groundCheckRange;
    [SerializeField] private float wallCheckRange;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask whatIsGround;
    

    ///////////////////////////////////////////

    private void Start()
    {
        //alive = transform.Find("alive").gameObject;
        aliveAnimator = alive.gameObject.GetComponent<Animator>();
        healthBar.setMaxHealth((int)maxHealth);
        currentHealth = maxHealth;
        rb = alive.GetComponent<Rigidbody2D>();
        facingDirection = 1;
    }

    private void Update() 
    {
        switch(currentState)
        {
            case State.walking:
                updateWalkingState();
                break;

            case State.takingDamage:
                updateTakingDamageState();
                break;

            case State.dead:
                updateDeadState();
                break;


        }

        updateAnimations();
    }

    private void updateAnimations()
    {
        aliveAnimator.SetFloat("horizontalVelocity", Mathf.Abs(rb.velocity.x));
    }
    
    private void enterWalkingState()
    {
    }

    private void updateWalkingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRange, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckRange, whatIsGround);

        if(!groundDetected || wallDetected)
        {
            flip();
        }
        else
        {
            rb.velocity = new Vector2(movementSpeed * facingDirection, rb.velocity.y);

        }
    }

    private void exitWalkingState()
    {

    }

    ///////////////////////////////////////////

    private void enterTakingDamageState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        rb.velocity = movement;
        aliveAnimator.SetBool("knockback", true);
    }

    private void updateTakingDamageState()
    {
        if(Time.time >= knockbackStartTime + knockbackDuration)
        {
            switchState(State.walking);
        }
    }

    private void exitTakingDamageState()
    {
        aliveAnimator.SetBool("knockback", false);
    }

    ///////////////////////////////////////////

    private void enterDeadState()
    {
        Destroy(gameObject);
    }

    private void updateDeadState()
    {

    }

    private void exitDeadState()
    {

    }

    ///////////////////////////////////////////

    private void switchState(State state)
    {
        switch(currentState)
        {
            case State.walking:
                exitWalkingState();
                break;

            case State.takingDamage:
                exitTakingDamageState();
                break;

            case State.dead:
                exitDeadState();
                break;
        }

        switch (state)
        {
            case State.walking:
                enterWalkingState();
                break;

            case State.takingDamage:
                enterTakingDamageState();
                break;

            case State.dead:
                enterDeadState();
                break;
        }

        currentState = state;
    }

    private void flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void takeDamage(float [] attackDetails)
    {
        Debug.Log("TAKING DMG");
        currentHealth -= attackDetails[0];
        healthBar.setHealth((int)currentHealth);

        if (attackDetails[1] > alive.transform.position.x) damageDirection = -1;
        else damageDirection = 1;

        if(currentHealth > 0f)
        {
            switchState(State.takingDamage);
        }
        else if(currentHealth >= 0)
        {
            switchState(State.dead);
        }
        
    }

    

        private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckRange));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckRange, wallCheck.position.y));
    }

}
