using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected bool isPLayerInMinAgroRange;
    protected bool isFinished;
    protected Transform attackPoint;

    public AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPoint) : base(entity, stateMachine, animBoolName)
    {
        this.attackPoint = attackPoint;
    }

    public override void Enter()
    {
        base.Enter();
        isFinished = false;
        entity.animationToStateMachine.attackState = this;
        isPLayerInMinAgroRange = entity.checkPlayerInMinAgroRange();
        entity.setVelocity(0f);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        isPLayerInMinAgroRange = entity.checkPlayerInMinAgroRange();
    }

    public virtual void triggerAttack()
    {
    }

    public virtual void finishAttack()
    {
        isFinished = true;
    }
}

