using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class Yandex : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void GetPlayerName();

    [SerializeField] private TextMeshProUGUI nameText;

    private void Start()
    {
        GetPlayerName();
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }
}
