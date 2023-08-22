using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tutorial
{
    public class TutorialMessages : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private List<string> textLines;
        [SerializeField] private TextMeshProUGUI textHolder;

        private int _currentTextIndex = 0;

        private PlayerMotor _playerM;

        private void Start()
        {
            _playerM = FindObjectOfType<PlayerMotor>();
            
            textHolder.text = textLines[_currentTextIndex];

            Time.timeScale = 0;
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            _currentTextIndex++;
            if (_currentTextIndex >= textLines.Count)
            {
                transform.parent.gameObject.SetActive(false);
                Time.timeScale = 1;
                return;
            }

            textHolder.text = textLines[_currentTextIndex];
        }

        public void ShowText(string text)
        {
            transform.parent.gameObject.SetActive(true);
            textHolder.text = text;
            Time.timeScale = 0;
        }
    }
}
