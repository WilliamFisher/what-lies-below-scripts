using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonChaseBehavior : SkeletonBaseFSM
{
    public override void OnStateEnter(Skeleton skeleton)
    {
        skeleton.enemyAnimator.SetBool("Chase", true);
        skeleton.enemyAgent.speed = 4f;
    }

    public override void OnStateExit(Skeleton skeleton)
    {
        skeleton.enemyAnimator.SetBool("Chase", false);
    }

    public override void OnStateUpdate(Skeleton skeleton)
    {
        if (skeleton.enemyAgent.enabled)
        {
            skeleton.MoveToPointInGameWorld(skeleton.playerTarget.transform.position);
        }

        if (!skeleton.isPlayerInFieldOfView())
        {
            OnStateExit(skeleton);
            skeleton.MakeTransitionToNextStateInFSM(skeleton.patrolBehavior);
        }

        if(skeleton.GetSquareMagnitudeDistanceOfObject(skeleton.playerTarget.transform.position, skeleton.transform.position) < EnemyConstants.ENEMY_ATTACK_DISTANCE)
        {
            OnStateExit(skeleton);
            skeleton.MakeTransitionToNextStateInFSM(skeleton.attackBehavior);
        }
    }
}
