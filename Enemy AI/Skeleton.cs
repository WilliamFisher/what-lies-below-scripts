using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    public Vector3 pointOfInterest;

    public SkeletonBaseFSM currentState;

    public SkeletonPatrolBehavior patrolBehavior = new SkeletonPatrolBehavior();
    public SkeletonChaseBehavior chaseBehavior = new SkeletonChaseBehavior();
    public SkeletonAttackBehavior attackBehavior = new SkeletonAttackBehavior();

    [SerializeField]
    private GameObject _ragdollPrefab;

    public override void Start()
    {
        base.Start();

        pointOfInterest = transform.position;

        MakeTransitionToNextStateInFSM(patrolBehavior);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateUpdate(this);
    }

    public void MakeTransitionToNextStateInFSM(SkeletonBaseFSM stateToMove)
    {
        currentState = stateToMove;
        currentState.OnStateEnter(this);
    }

    public bool isPlayerInFieldOfView()
    {
        bool canSeePlayer = false;
        Vector3 targetDirection = Vector3.forward;
        try
        {
            targetDirection = playerTarget.transform.position - transform.position;
        }
        catch (System.NullReferenceException)
        {
            while(playerTarget == null)
            {
                UpdatePlayerTarget();
            }
            targetDirection = playerTarget.transform.position - transform.position;
        }
        float currentViewAngle = Vector3.Angle(transform.forward, targetDirection);

        if (currentViewAngle < EnemyConstants.ENEMY_FOV_ANGLE &&
            GetSquareMagnitudeDistanceOfObject(playerTarget.transform.position, transform.position) < EnemyConstants.ENEMY_FOV_DISTANCE)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, targetDirection, out hit, EnemyConstants.ENEMY_RAY_LENGTH))
            {
                if(hit.collider.CompareTag("Player"))
                {
                    //Can see player
                    canSeePlayer = true;
                }
                else
                {
                    //Obstacle
                    canSeePlayer = false;
                }
            }
        }
        else
        {
            canSeePlayer = false;
        }
        

        return canSeePlayer;
    }

    public override void Die()
    {
        GameObject ragdoll = Instantiate(_ragdollPrefab, gameObject.transform.position, Quaternion.identity);
        Rigidbody rb = ragdoll.GetComponentInChildren<Rigidbody>();
        rb.AddForce(-Vector3.forward * 150, ForceMode.Impulse);
        Destroy(gameObject);
    }
}
