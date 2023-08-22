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
        
        private MoneyManager _moneyManager;
        private EnemiesManager _enemiesManager;

        private bool _coinsMessageShowed = false;
        private bool _enemiesMessageShowed = false;

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
                _coinsMessageShowed = true;
            }

            if (_enemiesManager.IsAnyEnemyAlive() == false && _enemiesMessageShowed == false)
            {
                tutorialMessages.ShowText(enemiesDeadText);
                _enemiesMessageShowed = true;
            }
        }
    }
}
