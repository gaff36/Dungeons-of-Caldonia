using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_MoveState : MoveState
{
    Wizard enemy;

    public Wizard_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Wizard enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (isPlayerInCircle)
        {

            stateMachine.ChangeState(enemy.playerDetectedState);
        }

        else if (isDetectingWall || !isDetectingGround)
        {
            enemy.idleState.setFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }     
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
