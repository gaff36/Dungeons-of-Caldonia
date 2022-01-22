using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public LayerMask whatIsStairs;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    public float wallCheckDistance = 0.2f;
    public float dodgeWallCheckDistance = 2f;
    public float groundCheckDistance = 0.2f;
    public float minAgroRange = 3f;
    public float maxAgroRange = 4f;
    public float meleeRange = 1f;
    public float playerCheckRange = 4f;
    public float maxHealth = 200f;
    public float damageHopSpeed = 3f;
    public float stunResistance = 50f;
    public float stunRecoveryTime = 2f;
    public float hurtRecoverytime = 0.1f;
    public float hurtSoundEffectCooldown = 1f;
    
}
