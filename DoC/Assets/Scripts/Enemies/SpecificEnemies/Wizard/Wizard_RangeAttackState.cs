using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_RangeAttackState : RangeAttackState
{
    private Wizard enemy;
    private GameObject fireball;
    private FireBall fireballScript;

    public Wizard_RangeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttackState stateData, Wizard enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
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

    public override void finishAttack()
    {
        base.finishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isFinished)
        {
            if (isPLayerInCircle)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void triggerAttack()
    {
        base.triggerAttack();

        fireball = GameObject.Instantiate(stateData.projectile, attackPoint.position, attackPoint.rotation);
        fireballScript = fireball.GetComponent<FireBall>();
        fireballScript.fireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage, attackDetails, entity.targetPosition);
    }
}
