using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelOnTrigger : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("MAX_LEVEL", levelNumber + 1);
            Loader.Load(Loader.Scenes.MainMenu);
        }
    }
}
