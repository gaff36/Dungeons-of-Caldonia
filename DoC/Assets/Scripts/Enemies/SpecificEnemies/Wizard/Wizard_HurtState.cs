using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_HurtState : hurtState
{
    private Wizard enemy;


    public Wizard_HurtState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Wizard enemy) : base(entity, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (entity.isStunned && !entity.isHurt) stateMachine.ChangeState(enemy.stunState);

        if (isPlayerInCircle && !entity.isHurt) stateMachine.ChangeState(enemy.playerDetectedState);
        else if (!entity.isHurt) stateMachine.ChangeState(enemy.moveState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
