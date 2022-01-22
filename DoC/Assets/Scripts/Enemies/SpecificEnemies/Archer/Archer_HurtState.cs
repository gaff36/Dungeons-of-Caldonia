using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_HurtState : hurtState
{
    private Archer enemy;


    public Archer_HurtState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Archer enemy) : base(entity, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("HURT");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (entity.isStunned && !entity.isHurt) stateMachine.ChangeState(enemy.stunState);
        
        else if (isPlayerInMinAgroRange && !entity.isHurt) stateMachine.ChangeState(enemy.playerDetectedState);
        else if (!entity.isHurt) stateMachine.ChangeState(enemy.moveState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
