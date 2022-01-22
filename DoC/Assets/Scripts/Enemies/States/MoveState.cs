using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingGround;
    protected bool isPLayerInMinAgroRange;
    protected bool isPLayerInMinAgroRangeBack;
    protected bool isPlayerInCircle;

    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        entity.setVelocity(stateData.movementSpeed);
        isDetectingWall = entity.checkWall();
        isDetectingGround = entity.checkGround();
        isPLayerInMinAgroRange = entity.checkPlayerInMinAgroRange();
        isPLayerInMinAgroRangeBack = entity.checkPlayerInMinAgroRangeBack();
        isPlayerInCircle = entity.checkPlayerCircle();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        isDetectingWall = entity.checkWall();
        isDetectingGround = entity.checkGround();
        isPLayerInMinAgroRange = entity.checkPlayerInMinAgroRange();
        isPLayerInMinAgroRangeBack = entity.checkPlayerInMinAgroRangeBack();
        isPlayerInCircle = entity.checkPlayerCircle();
    }

}