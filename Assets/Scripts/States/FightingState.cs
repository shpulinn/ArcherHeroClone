using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FightingState : BaseState
{
    private IdleState _idleState;
    private RunningState _runningState;
    private DeathState _deathState;
    private InputManager _inputManager;
    private int _motionAnimationID;

    [Header("Gun settings")]
    [SerializeField] private float damage = 4.0f;
    [SerializeField] private float energyCost = 0.2f;
    [SerializeField] private float reloadingTime = 1.0f;
    [SerializeField] private Transform projectileSpawnPos;
    [SerializeField] private Projectile projectilePrefab;
    [Space]
    [SerializeField] private float visionRadius = 5.0f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private LayerMask defaultLayerMask;
    [SerializeField] private Animator _animator;

    [Header("Draw shoot radius via line renderer")] [SerializeField]
    private bool showShootingRadius;

    [SerializeField] private List<EnemyHealth> enemyHealths = new List<EnemyHealth>();

    public Collider[] colliders;

    private bool _isReloading = false;
    private bool _isRotating = false;
    private Transform _nearestEnemy;
    private static readonly int Firing = Animator.StringToHash("Firing");

    private EnemiesManager _enemiesManager;

    private void Start()
    {
        _enemiesManager = FindObjectOfType<EnemiesManager>();
        
        _idleState = GetComponent<IdleState>();
        _runningState = GetComponent<RunningState>();
        _deathState = GetComponent<DeathState>();

        _motionAnimationID = Animator.StringToHash("Motion");
        
        _inputManager = InputManager.Instance;

        lineRenderer.positionCount = 51;
        lineRenderer.useWorldSpace = false;
        lineRenderer.enabled = false;
        if (showShootingRadius)
        {
            lineRenderer.enabled = true;
            CreatePoints();
        }
        
        //EnemyHealth.OnAnyEnemyDieEvent += OnAnyEnemyDieEvent;
    }

    private void OnAnyEnemyDieEvent(object sender, EventArgs e)
    {
        //_enemiesManager.RefreshEnemiesList();
        //colliders = _enemiesManager.GetEnemiesColliders();
    }

    // method for drawing player shooting radius via line renderer
    private void CreatePoints()
    {
        float x;
        float y;
        float z;

        float angle = 20f;

        for (int i = 0; i < (51); i++)
        {
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * visionRadius;
            y = Mathf.Cos (Mathf.Deg2Rad * angle) * visionRadius;

            lineRenderer.SetPosition (i,new Vector3(x,y,0) );

            angle += (360f / 51);
        }
    }
    
    public override void Construct()
    {
        stateName = "Fighting";
        
        colliders = _enemiesManager.GetEnemiesColliders();
    }

    public override void Transition()
    {
        // CREATE TRANSITION TO IDLE !!!

        //colliders = Physics.OverlapSphere(transform.position, visionRadius, enemyLayerMask);
        colliders = _enemiesManager.GetEnemiesColliders();
        foreach (var col in colliders)
        {
            Transform nearestEnemy = GetClosestEnemy(colliders);
            // Ray ray = new Ray(transform.position, nearestEnemy.transform.position);
            // RaycastHit hit;
            // if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayerMask))
            // {
            //     // there is obstacle between player and enemy, skip that 
            //     Debug.Log(hit.collider.name);
            //     continue;
            // }

            transform.LookAt(nearestEnemy);

            if (!_isReloading && nearestEnemy != null)
            {
                Shoot(nearestEnemy.GetComponent<EnemyHealth>());
            }
        }
        

        if (colliders.Length == 0 || _inputManager.Joystick)
        {
            playerMotor.IsFighting = false;
            _animator.SetBool(Firing, false);
        }

        if (playerMotor.IsFighting == false)
        {
            playerMotor.ChangeState(_idleState);
        }

        if (playerMotor.IsDead)
        {
            playerMotor.ChangeState(_deathState);
        }
    }

    private void Shoot(EnemyHealth enemyHealth)
    {
        if (enemyHealth == null)
        {
            playerMotor.ChangeState(_idleState);
            return;
        }
        playerMotor.StopMoving();
        _animator.SetBool("Firing", true);
        //enemyHealth.TakeDamage(damage);
        Vector3 aimDirection = (enemyHealth.transform.position - projectileSpawnPos.position).normalized + new Vector3(0, .1f,0);
        Instantiate(projectilePrefab.gameObject, projectileSpawnPos.position,
            Quaternion.LookRotation(aimDirection, Vector3.up));
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        _isReloading = true;
        yield return new WaitForSeconds(reloadingTime);
        _isReloading = false;
    }
    
    private Transform GetClosestEnemy (Collider[] enemies)
    {
        List<Transform> enemiesTransform = new List<Transform>();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                continue;
            }
            enemiesTransform.Add(enemies[i].transform);
        }
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(Transform potentialTarget in enemiesTransform)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
     
        return bestTarget;
    }
}
