using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float bulletDamage = 20f;
    [SerializeField] private Transform particlesEnemy;
    [SerializeField] private Transform particlesDefault;

    private Rigidbody _bulletRigidbody;

    public float BulletDamage
    {
        get => bulletDamage;
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
        // if (other.GetComponent<BulletTarget>() != null) {
        //     Instantiate(particlesEnemy, transform.position, Quaternion.identity);
        // } else {
        //     Instantiate(particlesDefault, transform.position, Quaternion.identity);
        // }
        Destroy(gameObject);
    }
}
