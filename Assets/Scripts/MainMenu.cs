using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowAdv();
    
    [SerializeField] private GameObject levelChoosingScreen;
    
    private void LoadTutorialScene()
    {
        Loader.Load(Loader.Scenes.TutorialScene);
    }

    public void PlayButtonClick()
    {
        if (PlayerPrefs.GetInt("TUTORIAL_COMPLETE") == 1)
        {
            levelChoosingScreen.SetActive(true);
            ShowAdv();
        }
        else
        {
            LoadTutorialScene();
        }
    }
}
