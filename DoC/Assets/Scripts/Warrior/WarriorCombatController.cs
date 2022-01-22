using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorCombatController : MonoBehaviour
{
    [SerializeField] private bool combatEnabled;
    [SerializeField] private float inputTimer;
    [SerializeField] private float attack1Radius;
    [SerializeField] private int attack1Damage;
    [SerializeField] private int stunDamage;
    [SerializeField] private Transform attack1Point;
    [SerializeField] private LayerMask whatIsDamageable;
    [SerializeField] private HealthBar healthBar;

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private int damageCooldown;
    [SerializeField] private WarriorController warriorController;

    [SerializeField] private float hurtRecoveryTime;
    private float hurtStartTime;

    [SerializeField] private GameManager gm;

   public bool takingDamage;
    public bool isDead;
    private bool gotInput;
    private bool attackButtonTriggered;
    public bool isAttacking;

    public int attackCounter;
    [SerializeField] private int attackSize;

    private float lastInputTime = Mathf.NegativeInfinity;
    private AttackDetails attackDetails;

    private Animator anim;
    [SerializeField] private AudioSource swing;
    [SerializeField] private AudioSource hurt;

    private void Start()
    {
        isDead = false;
        healthBar.setMaxHealth((int)maxHealth);
        attackCounter = 0;
        attackDetails = new AttackDetails();
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        anim.SetBool("dead", false);
        currentHealth = maxHealth;
         
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();

        if(takingDamage && Time.time >= hurtStartTime + hurtRecoveryTime)
        {
            stopTakingDamage();
        }

        if(currentHealth <= 0)
        {
            die();
        }
    }

    private void CheckCombatInput()
    {
        if (attackButtonTriggered)
        {
            attackButtonTriggered = false;

            if (combatEnabled)
            {
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks()
    {
        if (gotInput)
        {
            if (!isAttacking && !takingDamage)
            {
                gotInput = false;
                isAttacking = true;
                anim.SetTrigger("attack" + attackCounter);
                anim.SetBool("isAttacking", isAttacking);
                swing.Play();
               
            }
            
        }

        if (Time.time >= lastInputTime + inputTimer)
        {
            //Wait for new input
            gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1Point.position, attack1Radius, whatIsDamageable);

        attackDetails.damageAmount = attack1Damage;
        attackDetails.stunDamageAmount = stunDamage;
        attackDetails.position = transform.position;
        bool check;
        ArrayList colliderList = new ArrayList();

        foreach (Collider2D enemy in detectedObjects)
        {
            check = false;
            if(colliderList.Contains(enemy.transform.parent) == false)
            {
                colliderList.Add(enemy.transform.parent);
                check = true;
            }
            
            if (enemy.CompareTag("Enemy") && check)
            {
                enemy.transform.parent.GetComponent<Entity>().takeDamage(attackDetails);
                check = false;
            }
        }
    }

    public void FinishAttack1()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        //anim.SetBool("attack" + attackCounter, false);
        attackCounter = (attackCounter + 1) % (attackSize);
    }

    public void takeDamage(int damage)
    {
        if (!isDead)
        {
            hurt.Play();
            if (isAttacking)
            {
                FinishAttack1();
            }
            hurtStartTime = Time.time;
            takingDamage = true;
            currentHealth -= damage;
            healthBar.setHealth((int)currentHealth);
        }
    }

    public void stopTakingDamage()
    {
       takingDamage = false;
    }

    private void die()
    {
        if (!isDead)
        {
            gm.startDeadCanvas();
            isDead = true;
            warriorController.climbingLadder = false;
            anim.SetBool("dead", true);
        }
    }

    /// //////////////////////////////////////////////

    public void pressAttackButton()
    {
        attackButtonTriggered = true;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(attack1Point.position, attack1Radius);
    }
}
