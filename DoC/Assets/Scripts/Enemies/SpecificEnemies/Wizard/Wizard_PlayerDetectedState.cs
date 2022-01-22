using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_PlayerDetectedState : PlayerDetectedState
{
    private bool check;
    private Wizard enemy;

    public Wizard_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData, Wizard enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        check = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (entity.transform.position.x > entity.targetPosition.x && entity.facingDirection == 1) entity.flip();
        else if (entity.transform.position.x < entity.targetPosition.x && entity.facingDirection == -1) entity.flip();

        if (performMeleeAction && !check)
        {
            startTime = Time.time;
            check = true;
        }

        if (performMeleeAction && Time.time > startTime + enemy.meleeAttackStateData.meleeAttackCooldown)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        else if (Time.time >= startTime + enemy.rangeAttackStateData.rangeAttackCooldown)
        {
            stateMachine.ChangeState(enemy.rangeAttackState);
        }
        else if(!isPLayerInCircle)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
