using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private AttackDetails attackDetails;
    
    private float damageAmount;
    private Rigidbody2D rb;
    private bool hasHitTarget;
    private float travelDistance;
    private float angle;
    private bool hitPlayer;

    private Animator animator;

    [SerializeField] private float gravity;
    [SerializeField] private float speed;
    [SerializeField] private float damageRadius;
    [SerializeField] private Transform hitPosition;

    //

    private Vector2 targetPosition;
    private Vector2 startPos;

    //
    
    [SerializeField] private LayerMask whatIsPlayer;

    //

    private void Start()
    {

        animator = GetComponent<Animator>();
        attackDetails = new AttackDetails();
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        hasHitTarget = false;

        float angle = (-1 * (Mathf.Atan2(startPos.x - targetPosition.x, startPos.y - targetPosition.y) * Mathf.Rad2Deg));
             
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);


        rb.velocity = -10 * new Vector2(startPos.x - targetPosition.x, startPos.y - targetPosition.y).normalized;

    }

    private void FixedUpdate()
    {

            Collider2D damageHit = Physics2D.OverlapCircle(hitPosition.position, damageRadius, whatIsPlayer);
          
            if (damageHit && !hitPlayer)
            {
            if (damageHit.transform.GetComponent<WarriorController>().dashing == false)
            {
                hitPlayer = true;
                damageHit.transform.GetComponent<WarriorController>().damageHop(transform.position.x);
                damageHit.transform.GetComponent<WarriorCombatController>().takeDamage((int)damageAmount);
                animator.SetBool("boom", true);
            }
            }
            if (Mathf.Abs(startPos.x - transform.position.x) >= travelDistance)
            {
                animator.SetBool("boom", true);
            }

        
    }

    public void boom()
    {
        Destroy(gameObject);
    }

    public void fireProjectile(float speed, float travelDistance, float damage, AttackDetails attackDetails, Vector2 targetPosition)
    {
        hitPlayer = false;
        this.targetPosition = targetPosition;
        this.attackDetails = attackDetails;
        this.damageAmount = damage;
        this.speed = speed;
        this.travelDistance = travelDistance;
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitPosition.position, damageRadius);
    }
}
