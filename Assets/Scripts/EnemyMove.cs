using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public EventHandler OnPointReached;
    
    [SerializeField] private EnemyMovementType enemyMovementType;
    
    private NavMeshAgent _agent;

    private float _movingRadius = 10;

    private EnemyAttack _enemyAttack;

    private bool _waiting = false;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyAttack = GetComponent<EnemyAttack>();
        
        _enemyAttack.OnAttacked += OnAttacked;
    }

    private void Start()
    {
        switch (enemyMovementType)
        {
            case EnemyMovementType.Walking:
                _agent.SetDestination (AIPointGenerator.Instance.GetRandomPoint (transform, _movingRadius));
                break;
        }
    }

    private void OnAttacked(object sender, EventArgs e)
    {
        //_agent.SetDestination (AIPointGenerator.Instance.GetRandomPoint (transform, _movingRadius));
        _waiting = false;
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        switch (enemyMovementType)
        {
            case EnemyMovementType.Walking:
                if (!_agent.hasPath && _waiting == false)
                {
                    if (_agent.pathStatus.Equals(NavMeshPathStatus.PathComplete))
                    {
                        _waiting = true;
                        OnPointReached?.Invoke(this, EventArgs.Empty);
                        //return;
                    }
                    _agent.SetDestination (AIPointGenerator.Instance.GetRandomPoint (transform));
                }
                break;
                
        }
    }
}

public enum EnemyMovementType
{
    Static,
    Walking,
    Jumping,
    Flying
}
