using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_PlayerDetectedState : PlayerDetectedState
{
    private Archer enemy;
    private float dest;

    public Archer_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData, Archer enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        dodgeGroundCheck = entity.checkDodgeGround(2f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        dodgeGroundCheck = entity.checkDodgeGround(2f);
       
        if (performMeleeAction && Time.time >= enemy.dodgeState.startTime + enemy.dodgeStateData.dodgeCooldown && !dodgeWallCheck && dodgeGroundCheck)
        {
            stateMachine.ChangeState(enemy.dodgeState);
        }
        else if(isPLayerInMaxAgroRange && Time.time >= startTime + enemy.rangeAttackStateData.rangeAttackCooldown)
        { 
            stateMachine.ChangeState(enemy.rangeAttackState);
        }
        else if(!isPLayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.idleState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    
}
