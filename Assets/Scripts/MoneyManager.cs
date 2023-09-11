using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    [SerializeField] private TextMeshProUGUI coinsText;
    
    private int _currentMoney;
    
    void Start()
    {
        Instance = this;

        _currentMoney = PlayerPrefs.GetInt("MONEY", 0);
        
        UpdateUI();
    }

    public void AddMoney(int amount)
    {
        _currentMoney += amount;
        PlayerPrefs.SetInt("MONEY", _currentMoney);
        UpdateUI();
    }

    public bool TrySpendMoney(int amount)
    {
        if (_currentMoney - amount < 0)
        {
            return false;
        }

        _currentMoney -= amount;
        PlayerPrefs.SetInt("MONEY", _currentMoney);
        UpdateUI();
        return true;
    }

    public int GetCurrentMoney()
    {
        return _currentMoney;
    }

    private void UpdateUI()
    {
        coinsText.text = _currentMoney.ToString();
    }
}
