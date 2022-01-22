using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCombat : MonoBehaviour
{
    [SerializeField] private CharacterController2D controller;
    [SerializeField] private Animator heroAnimator;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private HeroMovement hero;

    private void Start()
    {
        hero = GetComponent<HeroMovement>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine("attack1");
        }
    }
  
    IEnumerator attack1()
    {     
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        controller.isAttacking = true;

        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy.CompareTag("Enemy"))
            enemy.GetComponent<Enemy>().takeDamage(20);
        }

        hero.isAttacking = true;
        heroAnimator.SetTrigger("Attack1");
        yield return new WaitForSeconds(0.4f);
        hero.isAttacking = false;
        controller.isAttacking = false;
    }

        private void OnDrawGizmos()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
   

}
