using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    public HeroMovement heroMovement;
    public Animator heroAnimator;
    [SerializeField] private int maxHealth = 200;
    [SerializeField] private int currentHealth;
    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        //healthBar.setMaxHealth(maxHealth);
    }

    public void takeDamage(int damage)
    {
        heroMovement.crouch = false;
        heroAnimator.SetBool("Crouch", false);
        heroMovement.takingDamageByObstacle = true;
        heroAnimator.SetTrigger("TakeHit");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            die();
        }
    }

    public void stopTakingDamage()
    {
        heroMovement.takingDamageByObstacle = false;
    }

    void die()
    {
    }
}
