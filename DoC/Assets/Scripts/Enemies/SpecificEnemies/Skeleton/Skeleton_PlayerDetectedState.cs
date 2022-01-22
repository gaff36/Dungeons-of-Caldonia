using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_PlayerDetectedState : PlayerDetectedState
{
    private Skeleton enemy;

    public Skeleton_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData, Skeleton enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (performMeleeAction && Time.time > startTime + enemy.meleeAttackStateData.meleeAttackCooldown)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }  
        else if(performRangeAction)
        {
            stateMachine.ChangeState(enemy.chargeState);
        }
        else if (!isPLayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.idleState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
