using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_StunState : StunState
{
    private Archer enemy;


    public Archer_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Archer enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("STUNNED");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("STUNNED EXIT");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
       
        if (!entity.isStunned)
        {      /*
            if (performMeleeAction && !entity.isStunned)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
            else if (isPlayerInMinAgroRange && !entity.isStunned)
            {
                stateMachine.ChangeState(enemy.chargeState);
            }       */
            if (!entity.isStunned)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }
        else if(entity.isHurt)
        {
            stateMachine.ChangeState(enemy.hurtState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
