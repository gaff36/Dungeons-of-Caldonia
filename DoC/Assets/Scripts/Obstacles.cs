using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private BoxCollider2D bc;
    private WarriorCombatController warriorCombatController;
    private WarriorController warriorController;
    private float lastDamageTime;
    private float damageDirection;
    [SerializeField] private float damageCooldown;
    [SerializeField] private float damageAmount;
    

    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
            warriorCombatController = collision.gameObject.GetComponent<WarriorCombatController>();
            warriorController = collision.gameObject.GetComponent<WarriorController>();
            if(warriorCombatController != null && !warriorCombatController.takingDamage )
            {
                lastDamageTime = Time.time;
                warriorCombatController.takeDamage((int)damageAmount);
                if (!warriorController.isFacingRight) damageDirection = -1f;
                else damageDirection = 1f;
                warriorController.damageHop(warriorController.transform.position.x + damageDirection);
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (warriorCombatController != null && !warriorCombatController.takingDamage && Time.time >= lastDamageTime + damageCooldown)
            {
                lastDamageTime = Time.time;
                warriorCombatController.takeDamage((int)damageAmount);
                if (!warriorController.isFacingRight) damageDirection = -1f;
                else damageDirection = 1f;
                warriorController.damageHop(warriorController.transform.position.x + damageDirection);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {

      
    }
}
