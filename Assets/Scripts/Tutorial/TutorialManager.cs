using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        public static TutorialManager Instance;

        [SerializeField] private TutorialMessages tutorialMessages;
        [SerializeField] private string coinsCollectedText;
        [SerializeField] private string enemiesDeadText;
        [SerializeField] private Animator doorAnimator;
        [SerializeField] private ParticleSystem doorParticles; 
        [SerializeField] private AudioClip doorOpeningSound;

        private MoneyManager _moneyManager;
        private EnemiesManager _enemiesManager;

        private bool _coinsMessageShowed = false;
        private bool _enemiesMessageShowed = false;
        private bool _doorOpenSoundPlayed = false;

        private void Awake()
        {
            Instance = this;

            _moneyManager = FindObjectOfType<MoneyManager>();
            _enemiesManager = FindObjectOfType<EnemiesManager>();
        }

        private void Update()
        {
            if (_moneyManager.GetCurrentMoney() == 3 && _coinsMessageShowed == false)
            {
                tutorialMessages.ShowText(coinsCollectedText);
                doorAnimator.SetTrigger("Open");
                doorParticles.Play();
                _coinsMessageShowed = true;
            }

            if (_moneyManager.GetCurrentMoney() == 3 && Time.timeScale == 1 && _doorOpenSoundPlayed == false)
            {
                AudioSource.PlayClipAtPoint(doorOpeningSound, transform.position);
                _doorOpenSoundPlayed = true;
            }

            if (_enemiesManager.IsAnyEnemyAlive() == false && _enemiesMessageShowed == false)
            {
                tutorialMessages.ShowText(enemiesDeadText);
                _enemiesMessageShowed = true;
            }
        }
    }
}
