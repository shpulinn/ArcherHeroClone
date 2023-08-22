using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void LoadTutorialScene()
    {
        Loader.Load(Loader.Scenes.TutorialScene);
    }

    public void LoadGameScene()
    {
        // check for player's progress ans load level
        Loader.Load(Loader.Scenes.GameScene);
    }
}
