using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]
public class D_MeleeAttackState : ScriptableObject
{
    public LayerMask whatIsPlayer;
    public float attackDamage = 20f;
    public float attackRadius = 0.5f;
    public float meleeAttackCooldown = 0.2f;
}