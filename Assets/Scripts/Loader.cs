using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scenes
    {
        GameScene,
        TutorialScene,
        MainMenu,
        Loading
    }

    private static Scenes _targetScene;

    public static void Load(Scenes targetScene)
    {
        _targetScene = targetScene;

        SceneManager.LoadScene(Scenes.Loading.ToString());
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(_targetScene.ToString());
    }
}
