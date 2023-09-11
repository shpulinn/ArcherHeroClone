using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public EventHandler OnPlayerDead;

    [DllImport("__Internal")]
    private static extern void ShowAdv();
    
    [SerializeField] private PlayerStatsSO playerStatsSO;
    [SerializeField] private float maxHealthPoints = 10.0f;
    [SerializeField] private ParticleSystem bloodParticles;
    //[SerializeField] private float timeToDestroyAfterDeath = 2.0f;
    [SerializeField] private Image healthImageBar;

    private float _currentHP;
    private Animator _animator;

    private PlayerMotor _playerMotor;
    //private EnemyMovement _enemyMovement;

    private int takeDamageAnimationTriggerID;
    private int isDeadAnimationID;

    private void Awake()
    {
        _playerMotor = GetComponent<PlayerMotor>();
        _animator = GetComponentInChildren<Animator>();

        maxHealthPoints = playerStatsSO.health;
        _currentHP = maxHealthPoints;
        healthImageBar.fillAmount = _currentHP;
        AssignAnimationsID();
    }
    
    private void AssignAnimationsID()
    {
        takeDamageAnimationTriggerID = Animator.StringToHash("Hit");
        isDeadAnimationID = Animator.StringToHash("IsDead");
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
            //OnAnyEnemyDieEvent?.Invoke(this, this);
        }
    }

    public void TakeHealing(float healing)
    {
        _currentHP += healing;
        if (_currentHP > maxHealthPoints)
        {
            _currentHP = maxHealthPoints;
        }
        healthImageBar.fillAmount = _currentHP / maxHealthPoints;
    }

    private void Die()
    {
        _playerMotor.IsDead = true;
        _animator.SetBool(isDeadAnimationID, true);
        OnPlayerDead?.Invoke(this, EventArgs.Empty);
        ShowAdv();
        //Destroy(gameObject);
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Enemy"))
    //     {
    //         TakeDamage(2);
    //     }
    // }
}
