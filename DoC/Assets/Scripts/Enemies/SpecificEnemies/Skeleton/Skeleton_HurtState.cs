using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_HurtState : hurtState
{
    private Skeleton enemy;


    public Skeleton_HurtState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Skeleton enemy) : base(entity, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("SKELETON HURT");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(entity.isStunned && !entity.isHurt) stateMachine.ChangeState(enemy.stunState);
        else if (performMeleeAction && !entity.isHurt) stateMachine.ChangeState(enemy.meleeAttackState);
        else if (isPlayerInMinAgroRange && !entity.isHurt) stateMachine.ChangeState(enemy.playerDetectedState);
        else if(!entity.isHurt) stateMachine.ChangeState(enemy.moveState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
