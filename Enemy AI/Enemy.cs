using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, IEnemyDamagable
{
    public Animator enemyAnimator;
    public NavMeshAgent enemyAgent;
    public GameObject playerTarget;

    private float _health;
    [SerializeField]
    private int experienceRewardOnDealth = 0;
    [SerializeField]
    private float _startingHealth = 100;

    void OnEnable()
    {
        CharacterDatabase.onCharacterChanged += UpdatePlayerTarget;
    }

    void OnDisable()
    {
        CharacterDatabase.onCharacterChanged -= UpdatePlayerTarget;
    }

    public void UpdatePlayerTarget()
    {
        try
        {
            playerTarget = GameObject.FindGameObjectWithTag("Player");
        }
        catch (NullReferenceException)
        {
            Debug.Log("No player target found in Enemy.cs");
            while(playerTarget == null)
            {
                playerTarget = GameObject.FindGameObjectWithTag("Player");
            }
        }
        
    }

    public virtual void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyAgent = GetComponent<NavMeshAgent>();
        UpdatePlayerTarget();
        _health = _startingHealth;
    }

    public virtual void Die() { }

    public void TakeDamage(float damage, GameObject characterObj)
    {
        _health -= damage;
        if (_health <= 0)
        {
            characterObj.GetComponent<CharacterBase>().Character.Experience += experienceRewardOnDealth;
            Die();
        }
    }

    public float GetSquareMagnitudeDistanceOfObject(Vector3 playerPos, Vector3 enemyPostion)
    {
        return (playerPos - enemyPostion).sqrMagnitude;
    }

    public void MoveToPointInGameWorld(Vector3 destinationPoint)
    {
        enemyAgent.destination = destinationPoint;
    }

    public Vector3 GetRandomPostionWithinRadius(Vector3 poi, float patrolRadius)
    {
        Vector3 randomPositionOnWorld = UnityEngine.Random.insideUnitSphere * patrolRadius;

        randomPositionOnWorld += poi;

        return new Vector3(randomPositionOnWorld.x, 0, randomPositionOnWorld.z);
    }

}
