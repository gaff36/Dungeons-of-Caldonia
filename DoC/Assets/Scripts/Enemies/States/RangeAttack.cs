using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : AttackState
{
    protected D_RangeAttackState stateData;
    protected bool isPLayerInCircle;
    
    protected AttackDetails attackDetails;

    public RangeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        isPLayerInCircle = entity.checkPlayerCircle();
        attackDetails = new AttackDetails();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void finishAttack()
    {
        base.finishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        isPLayerInCircle = entity.checkPlayerCircle();

    }

    public override void triggerAttack()
    {
        base.triggerAttack();
        entity.attackSound1.Play();
    }
}