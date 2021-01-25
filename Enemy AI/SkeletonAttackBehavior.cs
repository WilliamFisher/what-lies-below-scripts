using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackBehavior : SkeletonBaseFSM
{
    public override void OnStateEnter(Skeleton skeleton)
    {
        skeleton.enemyAnimator.SetBool("Attack", true);

        if (skeleton.enemyAgent.enabled)
        {
            skeleton.enemyAgent.isStopped = true;
        }
    }

    public override void OnStateExit(Skeleton skeleton)
    {
        skeleton.enemyAnimator.SetBool("Attack", false);

        if (skeleton.enemyAgent.enabled)
        {
            skeleton.enemyAgent.isStopped = false;
        }
    }

    public override void OnStateUpdate(Skeleton skeleton)
    {
        skeleton.transform.LookAt(new Vector3(skeleton.playerTarget.transform.position.x, skeleton.transform.position.y, skeleton.playerTarget.transform.position.z));


        if (skeleton.GetSquareMagnitudeDistanceOfObject(skeleton.playerTarget.transform.position, skeleton.transform.position) > EnemyConstants.ENEMY_ATTACK_DISTANCE)
        {
            OnStateExit(skeleton);
            skeleton.MakeTransitionToNextStateInFSM(skeleton.chaseBehavior);
        }
    }
}
