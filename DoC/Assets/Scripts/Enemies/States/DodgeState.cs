using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State
{
    public D_DodgeState stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool isGrounded;
    protected bool isDodgeGrounded;
    protected bool isDodgeWall;
    protected bool isDodgeOver;
    

    public DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        isDodgeGrounded = entity.checkDodgeWall();
        isDodgeWall = entity.checkDodgeWall();

        isGrounded = entity.checkGround();
        isPlayerInMaxAgroRange = entity.checkPlayerInMaxAgroRange();
        isPlayerInMinAgroRange = entity.checkPlayerInMaxAgroRange();


        if (isDodgeGrounded || !isDodgeWall)
        {
            Debug.Log("FINISH" + entity.gameObject.transform.position.x);
            isDodgeOver = false;
            entity.setVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -1 * entity.facingDirection);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {

        base.LogicUpdate();
        isGrounded = entity.checkGround();
        isPlayerInMaxAgroRange = entity.checkPlayerInMaxAgroRange();
        isPlayerInMinAgroRange = entity.checkPlayerInMaxAgroRange();


        if (Time.time >= startTime + stateData.dodgeTime && isGrounded)
        {
            Debug.Log("FINISH" + entity.gameObject.transform.position.x);
            isDodgeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    
}
