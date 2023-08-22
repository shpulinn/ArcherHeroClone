using UnityEngine;

public class LoadMenuOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Loader.Load(Loader.Scenes.MainMenu);
        }
    }
}
