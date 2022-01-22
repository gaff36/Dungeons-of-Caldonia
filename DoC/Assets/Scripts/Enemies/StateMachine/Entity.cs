using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public AnimationToStateMachine animationToStateMachine;
    public D_Entity entityData;
    public FiniteStateMachine stateMachine;
    public Rigidbody2D rb;
    public Animator animator { get; private set; }
    public Animator effectAnimator;
    public GameObject aliveGameObject;
    public int facingDirection { get; private set; }
    public float positionOfDodgeGroundCheck;

    [SerializeField] private Transform wallCheck;
    public Transform playerCheck;
    public Transform playerCheck2;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform groundCheckBack;
    [SerializeField] private Transform dodgeWallCheck;
    [SerializeField] private Transform dodgeGroundCheck;
    [SerializeField] private HealthBar healthBar;

    //

    public AudioSource attackSound1;
    public AudioSource attackSound2;
    public AudioSource hurtSound;
    public AudioSource deathSound;

    public float lastHurtTime;

    //

    public int lastDamageDirection { get; private set; }
    public float currentHealth { get; private set; }
    private float currentStunResistance;
    public float lastDamageTime { get; private set; }
    public float lastStunDamageTime { get; private set; }
    public bool isStunned { get; private set; }
    public bool isHurt;
    protected bool isDead;

    private Vector2 velocityWorkspace;
    public Vector2 targetPosition;
    public virtual void Start()
    {
        isStunned = false;
        isHurt = false;
        currentHealth = entityData.maxHealth;
        healthBar.setMaxHealth((int)entityData.maxHealth);
        facingDirection = 1;
        //aliveGameObject = transform.Find("alive").gameObject;
        //rb = aliveGameObject.GetComponent<Rigidbody2D>();
        animator = aliveGameObject.GetComponent<Animator>();
        stateMachine = new FiniteStateMachine();
        currentStunResistance = entityData.stunResistance;
        //animationToStateMachine = aliveGameObject.GetComponent<AnimationToStateMachine >();
    }

    public virtual void Update()
    {
        if (Time.time >= lastStunDamageTime + entityData.stunRecoveryTime && isStunned) resetStunResistance();
        stateMachine.currentState.LogicUpdate();

        if (currentHealth < entityData.maxHealth) healthBar.gameObject.SetActive(true);
        effectAnimator.SetBool("stun", isStunned);
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void setVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkspace;
    }

    public virtual void setVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = velocityWorkspace;
    }

    public virtual bool checkWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGameObject.transform.right, entityData.wallCheckDistance, entityData.whatIsGround)
            || Physics2D.Raycast(wallCheck.position, aliveGameObject.transform.right, entityData.wallCheckDistance, entityData.whatIsStairs);
    }

    public virtual bool checkWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, -1 * aliveGameObject.transform.right, entityData.wallCheckDistance, entityData.whatIsGround)
            || Physics2D.Raycast(wallCheck.position, -1 * aliveGameObject.transform.right, entityData.wallCheckDistance, entityData.whatIsStairs);
    }

    public bool checkGround()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, entityData.groundCheckDistance, entityData.whatIsGround);
    }

    public bool checkGroundBack()
    {
        return Physics2D.Raycast(groundCheckBack.position, Vector2.down, entityData.groundCheckDistance, entityData.whatIsGround);
    }

    public virtual bool checkPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGameObject.transform.right, entityData.minAgroRange, entityData.whatIsPlayer)
        || Physics2D.Raycast(playerCheck2.position, aliveGameObject.transform.right, entityData.minAgroRange, entityData.whatIsPlayer);

    }

    public virtual bool checkPlayerInMinAgroRangeBack()
    {
        return Physics2D.Raycast(playerCheck.position, -1 * aliveGameObject.transform.right, entityData.minAgroRange, entityData.whatIsPlayer)
        || Physics2D.Raycast(playerCheck2.position, -1 * aliveGameObject.transform.right, entityData.minAgroRange, entityData.whatIsPlayer);
    }

    public virtual bool checkPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGameObject.transform.right, entityData.maxAgroRange, entityData.whatIsPlayer)
        || Physics2D.Raycast(playerCheck2.position, aliveGameObject.transform.right, entityData.maxAgroRange, entityData.whatIsPlayer);
    }

    public virtual bool checkPlayerInMaxAgroRangeBack()
    {
        return (Physics2D.Raycast(playerCheck.position, -1 * aliveGameObject.transform.right, entityData.maxAgroRange, entityData.whatIsPlayer))
        || (Physics2D.Raycast(playerCheck2.position, -1 * aliveGameObject.transform.right, entityData.maxAgroRange, entityData.whatIsPlayer));
    }

    public virtual bool checkPlayerInMeleeRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGameObject.transform.right, entityData.meleeRange, entityData.whatIsPlayer)
        || Physics2D.Raycast(playerCheck2.position, aliveGameObject.transform.right, entityData.meleeRange, entityData.whatIsPlayer);
    }

    public bool checkDodgeWall()
    {
        return Physics2D.Raycast(dodgeWallCheck.position, -1 * aliveGameObject.transform.right, entityData.dodgeWallCheckDistance, entityData.whatIsGround | entityData.whatIsStairs);
    }

    public bool checkDodgeGround(float position)
    {
        //return Physics2D.Raycast(new Vector2(groundCheck.position.x + 1 * facingDirection* position, groundCheck.position.y), Vector2.down, entityData.groundCheckDistance, entityData.whatIsGround);
        return Physics2D.Raycast(dodgeGroundCheck.position, Vector2.down, entityData.groundCheckDistance, entityData.whatIsGround);
    }

    public bool checkPlayerCircle()
    {
        Collider2D hitPoint = Physics2D.OverlapCircle(playerCheck.position, entityData.playerCheckRange, entityData.whatIsPlayer);
        if(hitPoint) targetPosition = hitPoint.transform.position;
        return hitPoint;
        //return Physics2D.OverlapCircle(playerCheck.position, entityData.playerCheckRange, entityData.whatIsPlayer);
    }

    public virtual void flip()
    {
        facingDirection *= -1;
        aliveGameObject.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void takeDamage(AttackDetails attackDetails)
    {
        
            isHurt = true;
            currentStunResistance -= attackDetails.stunDamageAmount;
            lastDamageTime = Time.time;
            if (!isStunned) lastStunDamageTime = lastDamageTime;

            currentHealth -= attackDetails.damageAmount;
            healthBar.setHealth((int)currentHealth);

            if (attackDetails.position.x > aliveGameObject.transform.position.x) lastDamageDirection = -1;
            else lastDamageDirection = 1;

            damageHop(entityData.damageHopSpeed, -1 * lastDamageDirection);

            if (currentHealth <= 0)
            {                
                isDead = true;
            }

            if (currentStunResistance <= 0)
            {
                isStunned = true;
            }
        

    }

    public virtual void damageHop(float speed, int direction)
    {
        if(facingDirection == 1 && checkPlayerInMaxAgroRange() && checkGroundBack())
        {
            velocityWorkspace.Set(speed * direction * -1, speed);
        }
        else if(facingDirection == 1 && checkPlayerInMaxAgroRangeBack() && checkGround())
        {
            velocityWorkspace.Set(speed * direction * -1, speed);
        }
        else if (facingDirection == -1 && checkPlayerInMaxAgroRangeBack() && checkGround())
        {
            velocityWorkspace.Set(speed * direction * -1, speed);
        }
        else if (facingDirection == -1 && checkPlayerInMaxAgroRange() && checkGroundBack())
        {
            velocityWorkspace.Set(speed * direction * -1, speed);
        }
        else
        {
            velocityWorkspace.Set(0f, speed);
        }
        
        rb.velocity = velocityWorkspace;
    }

    public virtual void resetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }


    public virtual void OnDrawGizmos()
    {

        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        //Gizmos.DrawLine(groundCheck.position, groundCheck.position + (Vector3)(Vector2.down * entityData.groundCheckDistance));

        //Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.minAgroRange));
        //Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(Vector2.right * -1 * facingDirection * entityData.minAgroRange));
        //Gizmos.DrawLine(dodgeWallCheck.position, dodgeWallCheck.position + (Vector3)(Vector2.right * -1 * facingDirection * entityData.dodgeWallCheckDistance));
        //Gizmos.DrawLine(dodgeGroundCheck.position, dodgeGroundCheck.position + (Vector3)(Vector2.down * entityData.groundCheckDistance));
        //Gizmos.DrawLine(groundCheck.position, groundCheck.position + (Vector3)(Vector2.down * entityData.groundCheckDistance));
        //Gizmos.DrawWireSphere(playerCheck.position, entityData.playerCheckRange);

    }

}
