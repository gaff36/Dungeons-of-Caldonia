using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_MeleeAttackState : MeleeAttackState
{
    private Skeleton enemy;
    

    public Skeleton_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData, Skeleton enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

    public override void finishAttack()
    {
        base.finishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isFinished)
        {
            if(isPLayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void triggerAttack()
    {
        base.triggerAttack();
    }
}
