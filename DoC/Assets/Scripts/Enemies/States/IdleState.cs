using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected bool flipAfterIdle;
    protected D_IdleState stateData;
    protected float idleTime;
    protected bool isIdleTimeOver;
    protected bool isPLayerInMinAgroRange;
    protected bool isPLayerInMinAgroRangeBack;
    protected bool isPlayerInCircle;

    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

       // Debug.Log("IDLE");

        entity.setVelocity(0f);
        isIdleTimeOver = false;
        setRandomIdleTime();
        isPLayerInMinAgroRange = entity.checkPlayerInMinAgroRange();
        isPLayerInMinAgroRangeBack = entity.checkPlayerInMinAgroRangeBack();
        isPlayerInCircle = entity.checkPlayerCircle();
    }

    public override void Exit()
    {
        base.Exit();
        if (flipAfterIdle && !(isPLayerInMinAgroRange || isPLayerInMinAgroRangeBack)) entity.flip();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + idleTime) isIdleTimeOver = true;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        isPLayerInMinAgroRange = entity.checkPlayerInMinAgroRange();
        isPLayerInMinAgroRangeBack = entity.checkPlayerInMinAgroRangeBack();
        isPlayerInCircle = entity.checkPlayerCircle();
    }

    public void setFlipAfterIdle(bool flipAfterIdle)
    {
        this.flipAfterIdle = flipAfterIdle;
    }

    private void setRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
