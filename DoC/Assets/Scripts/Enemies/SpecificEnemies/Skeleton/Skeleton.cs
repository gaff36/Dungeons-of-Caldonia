using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Entity
{
    public Skeleton_IdleState idleState { get; private set; }
    public Skeleton_MoveState moveState { get; private set; }
    public Skeleton_PlayerDetectedState playerDetectedState { get; private set; }
    public Skeleton_ChargeState chargeState { get; private set; }
    public Skeleton_MeleeAttackState meleeAttackState { get; private set; }
    public Skeleton_StunState stunState { get; private set; }
    public Skeleton_HurtState hurtState { get; private set; }
    public Skeleton_DeadState deadState { get; private set; }

    public Transform meleeAttackPoint;

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_ChargeState chargeStateData;
    public D_MeleeAttackState meleeAttackStateData;
    [SerializeField] private D_StunState stunStateData;
    [SerializeField] private D_DeadState deadStateData;



    public override void Start()
    {
        base.Start();
        moveState = new Skeleton_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Skeleton_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new Skeleton_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        chargeState = new Skeleton_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        meleeAttackState = new Skeleton_MeleeAttackState(this, stateMachine, "melee", meleeAttackPoint, meleeAttackStateData, this);
        stunState = new Skeleton_StunState(this, stateMachine, "stun", stunStateData, this);
        hurtState = new Skeleton_HurtState(this, stateMachine, "hurt", this);
        deadState = new Skeleton_DeadState(this, stateMachine, "dead", deadStateData, this);
        stateMachine.Initialize(moveState);
    }

    public override void takeDamage(AttackDetails attackDetails)
    {
        base.takeDamage(attackDetails);

        /*
        if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }   */
        //if(isStunned) stateMachine.ChangeState(stunState);
        //if(!isStunned) 
        if (isDead) stateMachine.ChangeState(deadState); 

        else stateMachine.ChangeState(hurtState);
    }

    public override void Update()
    {
        base.Update();
       
    }
}