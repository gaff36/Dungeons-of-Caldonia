using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public AttackState attackState;

   private void triggerAttack()
    {
        attackState.triggerAttack();
    }

    private void finishAttack()
    {
        attackState.finishAttack();
    }
}
