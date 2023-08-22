using System;
using UnityEngine;

namespace Tutorial
{
    public class TutorialTriggerText : MonoBehaviour
    {
        [SerializeField] private TutorialMessages tutorialMessages;
        [SerializeField] private string message;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                tutorialMessages.ShowText(message);
                Destroy(gameObject);
            }
        }
    }
}
