using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected D_MeleeAttackState stateData;
    protected AttackDetails attackDetails;
    protected bool isInCircle;
    protected bool isInMeleeRange;

    public MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        
        attackDetails = new AttackDetails();
        attackDetails.damageAmount = stateData.attackDamage;
        attackDetails.position = entity.aliveGameObject.transform.position;
        isInCircle = entity.checkPlayerCircle();
        isInMeleeRange = entity.checkPlayerInMeleeRange();
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        isInCircle = entity.checkPlayerCircle();
        isInMeleeRange = entity.checkPlayerInMeleeRange();
    }

    public override void triggerAttack()
    {
        bool check = true;
        base.triggerAttack();
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPoint.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach(Collider2D collider in detectedObjects)
        {
            if (collider.CompareTag("Player") && check)
            {
                if (collider.transform.GetComponent<WarriorController>().dashing == false)
                {
                    entity.attackSound1.Play();
                    check = false;
                    collider.transform.GetComponent<WarriorController>().damageHop(attackDetails.position.x);
                    collider.transform.GetComponent<WarriorCombatController>().takeDamage((int)attackDetails.damageAmount);
                }
            }
        }
    }
}
