using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<PlayerHealth>().OnPlayerDead += OnPlayerDead;
        
        gameObject.SetActive(false);
    }

    private void OnPlayerDead(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
    }
}
