using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_IdleState : IdleState
{
    private Skeleton enemy;
    public Skeleton_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Skeleton enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (isPLayerInMinAgroRange || isPLayerInMinAgroRangeBack)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isIdleTimeOver) stateMachine.ChangeState(enemy.moveState);
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
