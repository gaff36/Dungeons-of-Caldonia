using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected D_ChargeState stateData;
    protected bool isPLayerInMinAgroRange;
    protected bool isDetectingWall;
    protected bool isDetectingGround;
    protected bool isChargeTimeOver;
    protected bool performMeleeAction;

    public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        
        isPLayerInMinAgroRange = entity.checkPlayerInMinAgroRange();
        isDetectingGround = entity.checkGround();
        isDetectingWall = entity.checkWall();
        performMeleeAction = entity.checkPlayerInMeleeRange();
        entity.setVelocity(stateData.chargeSpeed);
        isChargeTimeOver = false;
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.chargeTime) isChargeTimeOver = true;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        isPLayerInMinAgroRange = entity.checkPlayerInMinAgroRange();
        isDetectingGround = entity.checkGround();
        isDetectingWall = entity.checkWall();
        performMeleeAction = entity.checkPlayerInMeleeRange();
    }
}
