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
    [SerializeField] private PlayerStatsSO PlayerStatsSO;
    [SerializeField] private float damage = 4.0f;
    [SerializeField] private float reloadingTime = 1.0f;
    [SerializeField] private Transform projectileSpawnPos;
    [SerializeField] private Projectile projectilePrefab;
    [Space] [SerializeField] private AudioClip firingSound;
    [SerializeField] private float fireSoundVolume = .3f;
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private LayerMask defaultLayerMask;
    [SerializeField] private Animator _animator;

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

        projectilePrefab.SetDamage(PlayerStatsSO.damage);

        //EnemyHealth.OnAnyEnemyDieEvent += OnAnyEnemyDieEvent;
    }

    private void OnAnyEnemyDieEvent(object sender, EventArgs e)
    {
        //_enemiesManager.RefreshEnemiesList();
        //colliders = _enemiesManager.GetEnemiesColliders();
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
            
            // if there no enemies on this level/stage/sector
            if (Vector3.Distance(transform.position, nearestEnemy.position) > 30f)
            {
                playerMotor.ChangeState(_idleState);
                return;
            }

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
        Vector3 aimDirection = (enemyHealth.transform.position - projectileSpawnPos.position).normalized + new Vector3(0, .1f,0);
        Instantiate(projectilePrefab.gameObject, projectileSpawnPos.position,
            Quaternion.LookRotation(aimDirection, Vector3.up));
        if (firingSound != null)
        {
            AudioSource.PlayClipAtPoint(firingSound, transform.position, fireSoundVolume);
        }
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
