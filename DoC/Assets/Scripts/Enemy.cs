using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int damage;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Animator animator;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask heroLayer;
    [SerializeField] private float scanRange;
    [SerializeField] private float attackRange;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);

        animator.SetTrigger("Hit");
        if (currentHealth <= 0) die();
    }

    void Update()
    {
        StartCoroutine("attack");
        StartCoroutine("wait");
    }

    void die()
    {
        healthBar.slider.gameObject.SetActive(false);
        animator.SetBool("Dead", true);
        //GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    IEnumerator attack()
    {
        

        Vector2 checkPos = attackPoint.position;
        RaycastHit2D heroFound = Physics2D.Raycast(checkPos, Vector2.right, scanRange, heroLayer);

        if (heroFound)
        {
            Debug.Log("ATTACKING");
            yield return new WaitForSeconds(0.5f);

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, heroLayer);

            //yield return new WaitForSeconds(0.75f);

            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.CompareTag("Player"))
                {
                    enemy.GetComponent<Hero>().takeDamage(damage);
                    break;
                }
            }
        }

    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
    }
    void attack1()
    {
        
        Vector2 checkPos = attackPoint.position;
        RaycastHit2D heroFound = Physics2D.Raycast(checkPos, Vector2.right, scanRange, heroLayer);

        if (heroFound)
        {
            StartCoroutine("attack");
        }
    }

    void scanHero()
    {
        StartCoroutine("attack");
        Vector2 checkPos = attackPoint.position;

        /*
        RaycastHit2D heroFound = Physics2D.Raycast(checkPos, Vector2.right, scanRange, heroLayer);

        if(heroFound)
        {
            StartCoroutine("attack");
        }

        Debug.DrawRay(checkPos, scanRange * Vector2.right, Color.blue);     */
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
