using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Entity
{
    public Archer_IdleState idleState { get; private set; }
    public Archer_MoveState moveState { get; private set; }
    public Archer_PlayerDetectedState playerDetectedState { get; private set; }
    public Archer_StunState stunState { get; private set; }
    public Archer_HurtState hurtState { get; private set; }
    public Archer_DeadState deadState { get; private set; }
    public Archer_DodgeState dodgeState { get; private set; }
    public Archer_RangeAttackState rangeAttackState { get; private set; }

    //////////////

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_StunState stunStateData;
    [SerializeField] private D_DeadState deadStateData;
    public D_RangeAttackState rangeAttackStateData;
    public D_DodgeState dodgeStateData;

    [SerializeField] private Transform rangedAttackPosition;

    public override void Start()
    {
        base.Start();

        idleState = new Archer_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new Archer_MoveState(this, stateMachine, "move", moveStateData, this);
        playerDetectedState = new Archer_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        stunState = new Archer_StunState(this, stateMachine, "stun", stunStateData, this);
        hurtState = new Archer_HurtState(this, stateMachine, "hurt", this);
        deadState = new Archer_DeadState(this, stateMachine, "dead", deadStateData, this);
        dodgeState = new Archer_DodgeState(this, stateMachine, "dodge", dodgeStateData, this);
        rangeAttackState = new Archer_RangeAttackState(this, stateMachine, "attack", rangedAttackPosition,  rangeAttackStateData, this);

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

        if (isDead && stateMachine.currentState != deadState) stateMachine.ChangeState(deadState);

        else stateMachine.ChangeState(hurtState);
            
    }

   

}
