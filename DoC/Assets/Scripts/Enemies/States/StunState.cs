using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    protected D_StunState stateData;
    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovementStopped;
    protected bool performMeleeAction;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInCircle;

    public StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        
        isStunTimeOver = false;
        isMovementStopped = false;
        entity.setVelocity(stateData.stunKnockBackSpeed, stateData.stunKnockbackAngle, entity.lastDamageDirection);
        isGrounded = entity.checkGround();
        performMeleeAction = entity.checkPlayerInMeleeRange();
        isPlayerInMinAgroRange = entity.checkPlayerInMinAgroRange();
        isPlayerInCircle = entity.checkPlayerCircle();
    }

    public override void Exit()
    {
        base.Exit();

        //entity.resetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= startTime + stateData.stunTime)
        {
            isStunTimeOver = true;
        }/*
        if (isGrounded && Time.time >= startTime + stateData.stunKnokbackTime && !isMovementStopped)
        {
            entity.setVelocity(0f);
            isMovementStopped = true;
        }*/
        if(isGrounded && !entity.isStunned && !isMovementStopped)
        {
            entity.setVelocity(0f);
            isMovementStopped = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        isGrounded = entity.checkGround();
        performMeleeAction = entity.checkPlayerInMeleeRange();
        isPlayerInMinAgroRange = entity.checkPlayerInMinAgroRange();
        isPlayerInCircle = entity.checkPlayerCircle();
    }
}
