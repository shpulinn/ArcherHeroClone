using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EventHandler OnAttacked;
    
    [SerializeField] private AttackType attackType;
    [SerializeField] private int damage;

    [Space] [Header("Range")] 
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private Transform projectileSpawnPos;

    [SerializeField] private float enemiesAttackRadius = 20f;

    [SerializeField] private float attackSpeed;

    private EnemyMove _enemyMove;

    private bool _attacking = false;

    private Transform _playerTransform;

    private void Awake()
    {
        _enemyMove = GetComponent<EnemyMove>();

        _playerTransform = FindObjectOfType<PlayerMotor>().transform;
        
        _enemyMove.OnPointReached += OnPointReached;
    }

    private void OnPointReached(object sender, EventArgs e)
    {
        switch (attackType)
        {
            case AttackType.Range:
                projectilePrefab.GetComponent<EnemiesProjectile>().SetDamage(damage);
                RangeAttackPlayer();
                break;
        }
    }

    private void RangeAttackPlayer()
    {
        if (!_attacking)
        {
            Vector3 aimDirection = (_playerTransform.position - projectileSpawnPos.position).normalized + new Vector3(0, .1f,0);
            Instantiate(projectilePrefab.gameObject, projectileSpawnPos.position,
                Quaternion.LookRotation(aimDirection, Vector3.up));
            //if (firingSound != null)
            //{
            //    AudioSource.PlayClipAtPoint(firingSound, transform.position, fireSoundVolume);
            //}
            //StartCoroutine(ReloadCoroutine());
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    private IEnumerator Attack()
    {
        _attacking = true;
        transform.LookAt(_playerTransform);
        yield return new WaitForSeconds(3f);
        OnAttacked?.Invoke(this, EventArgs.Empty);
        _attacking = false;
    }
}

public enum AttackType
{
    Melee,
    Range
}