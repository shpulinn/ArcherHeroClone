using UnityEngine;

public class HealingPickUp : MonoBehaviour
{
    [SerializeField] private float healing = 5f;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float rotationSpeed = 5f;

    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime * rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeHealing(healing);
            Destroy(gameObject);
        }
    } 
}
