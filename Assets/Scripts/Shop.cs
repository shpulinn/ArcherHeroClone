using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO playerStatsSO;
    [Space] 
    [SerializeField] private TextMeshProUGUI currentHealthText;
    [SerializeField] private TextMeshProUGUI currentHealthUpgradeCostText;
    [Space] 
    [SerializeField] private TextMeshProUGUI currentDamageText;
    [SerializeField] private TextMeshProUGUI currentDamageUpgradeCostText;
    [Space] 
    [SerializeField] private GameObject notEnoughMoneyMessage;

    private int _currentHealthUpgradeCost = 10;
    private int _currentDamageUpgradeCost = 10;

    private void Start()
    {
        currentHealthText.text = $"ТЕКУЩЕЕ ЗДОРОВЬЕ: {playerStatsSO.health}";
        currentDamageText.text = $"ТЕКУЩИЙ УРОН: {playerStatsSO.damage}";
    }

    public void UpgradeHealth()
    {
        if (MoneyManager.Instance.TrySpendMoney(_currentHealthUpgradeCost))
        {
            playerStatsSO.health += 5;
            PlayerPrefs.SetInt("HEALTH", playerStatsSO.health);
            currentHealthText.text = $"ТЕКУЩЕЕ ЗДОРОВЬЕ: {playerStatsSO.health}";
        }
        else
        {
            notEnoughMoneyMessage.SetActive(true);
            Invoke(nameof(HideMessage), 2f);
        }
    }

    public void UpgradeDamage()
    {
        if (MoneyManager.Instance.TrySpendMoney(_currentDamageUpgradeCost))
        {
            playerStatsSO.damage += 1;
            PlayerPrefs.SetInt("DAMAGE", playerStatsSO.damage);
            currentDamageText.text = $"ТЕКУЩИЙ УРОН: {playerStatsSO.damage}";
        }
        else
        {
            notEnoughMoneyMessage.SetActive(true);
            Invoke(nameof(HideMessage), 2f);
        }
    }

    private void HideMessage()
    {
        notEnoughMoneyMessage.SetActive(false);
    }
}
