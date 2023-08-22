using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coins = 1;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private AudioClip pickSound;

    private void Update()
    {
        transform.Rotate(rotation * (Time.deltaTime * rotationSpeed));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MoneyManager.Instance.AddMoney(coins);
            AudioSource.PlayClipAtPoint(pickSound, transform.position);
            Destroy(transform.parent.gameObject);
        }
    } 
}
