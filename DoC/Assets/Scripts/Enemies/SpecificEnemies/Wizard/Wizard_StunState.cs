using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_StunState : StunState
{
    private Wizard enemy;


    public Wizard_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Wizard enemy) : base(entity, stateMachine, animBoolName, stateData)
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
     
        if (!entity.isStunned)
        {
            if (performMeleeAction)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }    
            else if(isPlayerInCircle)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }
        else if (entity.isHurt)
        {
            stateMachine.ChangeState(enemy.hurtState);
        }
    }



    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
