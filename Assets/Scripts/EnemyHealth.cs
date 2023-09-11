using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour
{
    public static EventHandler<EnemyHealth> OnAnyEnemyDieEvent;
    
    [SerializeField] private float maxHealthPoints = 10.0f;
    [SerializeField] private ParticleSystem bloodParticles;
    [SerializeField] private float timeToDestroyAfterDeath = 2.0f;
    [SerializeField] private Image healthImageBar;
    [Space]
    [SerializeField] private GameObject moneyPrefab;

    private int moneyRange;

    private float _currentHP;
    private Animator _animator;
    //private EnemyMovement _enemyMovement;

    private int takeDamageAnimationTriggerID;
    private int isDeadAnimationID;

    private void Start()
    {
        _currentHP = maxHealthPoints;
        _animator = GetComponent<Animator>();
        //_enemyMovement = GetComponent<EnemyMovement>();

        moneyRange = Random.Range(1, 11);

        AssignAnimationsID();
    }

    private void AssignAnimationsID()
    {
        takeDamageAnimationTriggerID = Animator.StringToHash("Hit");
        //isDeadAnimationID = Animator.StringToHash("IsDead");
    }

    public void TakeDamage(float damage)
    {
        if (bloodParticles)
        {
            bloodParticles.Play();
        }

        if (_animator != null)
        {
            _animator.SetTrigger(takeDamageAnimationTriggerID);
        }
        _currentHP -= damage;
        healthImageBar.fillAmount = _currentHP / maxHealthPoints;
        if (_currentHP <= 0)
        {
            Die();
            OnAnyEnemyDieEvent?.Invoke(this, this);
        }
    }

    private void Die()
    {
        //_enemyMovement.DeathAction();
        //_animator.SetBool(isDeadAnimationID, true);
        //Destroy(gameObject, timeToDestroyAfterDeath);
        
        // Destroying this component and EnemyMovement component for "disabling" enemy, 
        // but leave on scene for "timeToDestroyAfterDeath" seconds after death and then disappear
        // Destroy(this);
        for (int i = 0; i < moneyRange; i++)
        {
            Instantiate(moneyPrefab, GetRandomPositionNear(), quaternion.identity);
        }
        Destroy(gameObject);
        //Destroy(_enemyMovement);
    }

    private Vector3 GetRandomPositionNear()
    {
        return new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y,
            transform.position.z + Random.Range(-1f, 1f));
    }
}