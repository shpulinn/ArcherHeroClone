using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesProjectile : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float bulletDamage = 20f;
    [SerializeField] private Transform particlesEnemy;
    [SerializeField] private Transform particlesDefault;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private float hitSoundVolume = .3f;

    private Rigidbody _bulletRigidbody;

    public float BulletDamage
    {
        get => bulletDamage;
    }

    public void SetDamage(int damage)
    {
        bulletDamage = damage;
    }

    private void Awake ()
    {
        _bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start ()
    {
        _bulletRigidbody.velocity = transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player") && particlesEnemy != null) {
            Instantiate(particlesEnemy, transform.position, Quaternion.identity);
        } else if (other.CompareTag("Untagged") && particlesDefault != null) {
            Instantiate(particlesDefault, transform.position, Quaternion.identity);
        }
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(bulletDamage);
        }

        if (hitSound != null)
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position, hitSoundVolume);
        }
        Destroy(gameObject);
    }
}
