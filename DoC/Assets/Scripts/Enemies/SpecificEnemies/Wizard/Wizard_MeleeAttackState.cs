using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_MeleeAttackState : MeleeAttackState
{
    private Wizard enemy;


    public Wizard_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData, Wizard enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        entity.attackSound2.Play();
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
        if (isFinished)
        {
   
            if (isInCircle)
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
