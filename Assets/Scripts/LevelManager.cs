using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public List<Button> levelButtons;

    private int _maxLevel;
    private void Start()
    {
        _maxLevel = PlayerPrefs.GetInt("MAX_LEVEL", 1);
        UpdateLevelButtons();
    }

    private void UpdateLevelButtons()
    {
        int maxLevel = _maxLevel;
        for (int i = 0; i < levelButtons.Count; i++)
        {
            levelButtons[i].interactable = (i + 1) <= maxLevel;
        }
    }
}
