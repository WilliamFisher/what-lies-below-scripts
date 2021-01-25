using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPatrolBehavior : SkeletonBaseFSM
{
    private float counter = 0;
    private float patrolTime = 3;

    public override void OnStateEnter(Skeleton skeleton)
    {
        skeleton.enemyAnimator.SetBool("Patrol", true);

        skeleton.enemyAgent.speed = 2f;

        Vector3 newPatrolPositon = skeleton.GetRandomPostionWithinRadius(skeleton.pointOfInterest, EnemyConstants.ENEMY_PATROL_RADIUS);

        skeleton.MoveToPointInGameWorld(newPatrolPositon);
    }

    public override void OnStateExit(Skeleton skeleton)
    {
        skeleton.enemyAnimator.SetBool("Patrol", false);
    }

    public override void OnStateUpdate(Skeleton skeleton)
    {
        counter += Time.deltaTime;

        if(counter >= patrolTime)
        {
            Vector3 newPatrolPositon = skeleton.GetRandomPostionWithinRadius(skeleton.pointOfInterest, EnemyConstants.ENEMY_PATROL_RADIUS);

            skeleton.MoveToPointInGameWorld(newPatrolPositon);

            counter = 0;
        }

        if (skeleton.isPlayerInFieldOfView())
        {
            OnStateExit(skeleton);
            skeleton.MakeTransitionToNextStateInFSM(skeleton.chaseBehavior);

        }
    }
}
