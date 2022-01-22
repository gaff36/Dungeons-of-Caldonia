using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_StunState : StunState
{
    private Skeleton enemy;


    public Skeleton_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Skeleton enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        /*
        if(isStunTimeOver)
        {
            if(performMeleeAction && !entity.isStunned)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
            else if(isPlayerInMinAgroRange && !entity.isStunned)
            {
                stateMachine.ChangeState(enemy.chargeState);
            }
            else if(!entity.isStunned)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }   */

        if (!entity.isStunned)
        {
            if (performMeleeAction && !entity.isStunned)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
            else if (isPlayerInMinAgroRange && !entity.isStunned)
            {
                stateMachine.ChangeState(enemy.chargeState);
            }
            else if (!entity.isStunned)
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
