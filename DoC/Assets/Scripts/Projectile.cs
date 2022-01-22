using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private AttackDetails attackDetails;
    private float speed;
    private float damageAmount;
    private Rigidbody2D rb;
    private float travelDistance;
    private float xStartPos;
    private bool isGravityOn;
    private bool hasHitGround;
    public int direction;


    [SerializeField] private Transform hitPosition;
    [SerializeField] private float gravity;
    [SerializeField] private float damageRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;

    private void Start()
    {
        attackDetails = new AttackDetails();
        
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        if (direction == -1) transform.rotation = Quaternion.Euler(Vector3.forward * 180f);

        rb.velocity = transform.right * speed;
    
        xStartPos = transform.position.x;
        isGravityOn = false;
    }

    private void Update()
    {
        if(!hasHitGround)
        {
            //attackDetails.position = transform.position;

            if (isGravityOn)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(hitPosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(hitPosition.position, damageRadius, whatIsGround);

            if(damageHit)
            {
                if (damageHit.transform.GetComponent<WarriorController>().dashing == false)
                {
                    //damageHit.transform.SendMessage("takeDamage", attackDetails.damageAmount);
                    damageHit.transform.GetComponent<WarriorController>().damageHop(transform.position.x);
                    damageHit.transform.GetComponent<WarriorCombatController>().takeDamage((int)damageAmount);
                    Destroy(gameObject);
                }
            }

            if(groundHit)
            {
                hasHitGround = true;
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
            }

            if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !isGravityOn)
            {
                isGravityOn = true;
                rb.gravityScale = gravity;
            }
        }
    }

    public void fireProjectile(float speed, float travelDistance, float damage, AttackDetails attackDetails)
    {
        /*
        this.speed = speed;
        this.travelDistance = travelDistance;
        attackDetails.damageAmount = damage;
        attackDetails.position = transform.position;   

        //attackDetails.position = transform.parent.position;
        attackDetails.damageAmount = damage;
        attackDetails.stunDamageAmount = 0f; */

        this.attackDetails = attackDetails;
        this.damageAmount = damage;
        this.speed = speed;
        this.travelDistance = travelDistance;
       
    }

}
