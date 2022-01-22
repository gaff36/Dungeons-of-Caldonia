using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected D_DeadState stateData;
    private float deathTime;

    public DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.deathSound.Play();
        GameObject.Instantiate(stateData.deathBloodParticle, entity.aliveGameObject.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        GameObject.Instantiate(stateData.deathChunkParticle, entity.aliveGameObject.transform.position, stateData.deathChunkParticle.transform.rotation);
        GameObject.Instantiate(stateData.bloodStain, new Vector2(entity.aliveGameObject.transform.position.x, entity.aliveGameObject.transform.position.y + 0.25f), stateData.bloodStain.transform.rotation);
        entity.gameObject.SetActive(false);
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
    }

}