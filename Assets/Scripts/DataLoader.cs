using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO playerStatsSO;

    private void Start()
    {
        playerStatsSO.damage = PlayerPrefs.GetInt("DAMAGE", 3);
        playerStatsSO.health = PlayerPrefs.GetInt("HEALTH", 10);
    }
}
