using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtState : State
{
    protected bool performMeleeAction;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInCircle;
    public hurtState(Entity entity, FiniteStateMachine stateMachine, string animBoolName) : base(entity, stateMachine, animBoolName)
    {    
    }

    public override void Enter()
    {
        base.Enter();
        if (Time.time >= entity.lastHurtTime + entity.entityData.hurtSoundEffectCooldown)
        {
            entity.hurtSound.Play();
            entity.lastHurtTime = startTime;
        }
        performMeleeAction = entity.checkPlayerInMeleeRange();
        isPlayerInMinAgroRange = entity.checkPlayerInMinAgroRange();
        isPlayerInCircle = entity.checkPlayerCircle();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        performMeleeAction = entity.checkPlayerInMeleeRange();
        isPlayerInMinAgroRange = entity.checkPlayerInMinAgroRange();
        isPlayerInCircle = entity.checkPlayerCircle();
        if (Time.time >= entity.lastDamageTime + entity.entityData.hurtRecoverytime) entity.isHurt = false;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
