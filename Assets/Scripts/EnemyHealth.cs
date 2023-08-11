using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static EventHandler<EnemyHealth> OnAnyEnemyDieEvent;
    
    [SerializeField] private float maxHealthPoints = 10.0f;
    [SerializeField] private ParticleSystem bloodParticles;
    [SerializeField] private float timeToDestroyAfterDeath = 2.0f;
    [SerializeField] private Image healthImageBar;

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
        _animator.SetTrigger(takeDamageAnimationTriggerID);
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
        Destroy(gameObject);
        //Destroy(_enemyMovement);
    }
}