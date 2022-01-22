using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState stateData;
    protected bool isPLayerInMinAgroRange;
    //protected bool isPLayerInMinAgroRangeBack;
    protected bool isPLayerInMaxAgroRange;
    protected bool isPLayerInCircle;
    //protected bool isPLayerInMaxAgroRangeBack;
    protected bool performRangeAction;
    protected bool performMeleeAction;
    protected bool dodgeWallCheck;
    protected bool dodgeGroundCheck;
    protected float lastRangeAttackTime;

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        //Debug.Log("PLEYER DETECTED");


        performRangeAction = false;
        entity.setVelocity(0f);
        isPLayerInMinAgroRange = entity.checkPlayerInMinAgroRange();
        //isPLayerInMinAgroRangeBack = entity.checkPlayerInMinAgroRangeBack();
        performMeleeAction = entity.checkPlayerInMeleeRange();
        dodgeWallCheck = entity.checkDodgeWall();      
        isPLayerInMaxAgroRange = entity.checkPlayerInMaxAgroRange();
        isPLayerInCircle = entity.checkPlayerCircle();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        dodgeWallCheck = entity.checkDodgeWall();
       
         if(Time.time >= startTime + stateData.actionTime)
         {
            performRangeAction = true;
         }


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        performMeleeAction = entity.checkPlayerInMeleeRange();    
        isPLayerInMinAgroRange = entity.checkPlayerInMinAgroRange();
        //isPLayerInMinAgroRangeBack = entity.checkPlayerInMinAgroRangeBack();
        isPLayerInMaxAgroRange = entity.checkPlayerInMaxAgroRange();
        isPLayerInCircle = entity.checkPlayerCircle();
    }
}
