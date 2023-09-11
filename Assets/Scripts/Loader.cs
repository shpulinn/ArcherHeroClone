using UnityEngine.SceneManagement;

public static class Loader
{
    private static int _levelNumber;
    private static bool _loadingGameLevel = false;
    
    public enum Scenes
    {
        TutorialScene,
        MainMenu,
        Loading,
        Level_
    }

    private static Scenes _targetScene;

    public static void Load(Scenes targetScene)
    {
        _targetScene = targetScene;

        SceneManager.LoadScene(Scenes.Loading.ToString());
    }

    public static void LoadLevel(int number)
    {
        _targetScene = Scenes.Level_;
        _levelNumber = number;
        _loadingGameLevel = true;
        SceneManager.LoadScene(Scenes.Loading.ToString());
    }

    public static void LoaderCallback()
    {
        if (_loadingGameLevel)
        {
            _loadingGameLevel = false;
            SceneManager.LoadScene(_targetScene.ToString() + _levelNumber);
        }
        else
        {
            SceneManager.LoadScene(_targetScene.ToString());
        }
    }
}
