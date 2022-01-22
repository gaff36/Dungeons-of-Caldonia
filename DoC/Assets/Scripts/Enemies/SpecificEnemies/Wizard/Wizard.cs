using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Entity
{
    public Wizard_IdleState idleState { get; private set; }
    public Wizard_MoveState moveState { get; private set; }
    public Wizard_PlayerDetectedState playerDetectedState { get; private set; }
    public Wizard_HurtState hurtState { get; private set; }
    public Wizard_DeadState deadState { get; private set; }
    public Wizard_RangeAttackState rangeAttackState { get; private set; }
    public Wizard_MeleeAttackState meleeAttackState { get; private set; }
    public Wizard_StunState stunState { get; private set; }

    [SerializeField] private Transform rangeAttackPosition;
    public Transform meleeAttackPosition;

    //

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_DeadState deadStateData;
    public D_RangeAttackState rangeAttackStateData;
    public D_MeleeAttackState meleeAttackStateData;
    [SerializeField] private D_StunState stunStateData;


    public override void Start()
    {
        base.Start();

        idleState = new Wizard_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new Wizard_MoveState(this, stateMachine, "move", moveStateData, this);
        playerDetectedState = new Wizard_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        hurtState = new Wizard_HurtState(this, stateMachine, "hurt", this);
        deadState = new Wizard_DeadState(this, stateMachine, "dead", deadStateData, this);
        rangeAttackState = new Wizard_RangeAttackState(this, stateMachine, "rangeAttack", rangeAttackPosition, rangeAttackStateData, this);
        meleeAttackState = new Wizard_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        stunState = new Wizard_StunState(this, stateMachine, "stun", stunStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void takeDamage(AttackDetails attackDetails)
    {
        base.takeDamage(attackDetails);

        if (isDead && stateMachine.currentState != deadState) stateMachine.ChangeState(deadState);

        else stateMachine.ChangeState(hurtState);

    }
}
