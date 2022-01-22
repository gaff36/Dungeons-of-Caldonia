using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_RangeAttackState : RangeAttackState
{
    private Archer enemy;
    protected GameObject projectile;
    protected Projectile projectileScript;
    public Archer_RangeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttackState stateData, Archer enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("RANGE ATTACK");
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
            if (isPLayerInMinAgroRange)
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
        projectile = GameObject.Instantiate(stateData.projectile, attackPoint.position, attackPoint.rotation);
        projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.direction = entity.facingDirection;
        projectileScript.fireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage, attackDetails);
    }
}
