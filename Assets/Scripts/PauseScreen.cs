using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    private bool _isPaused = false;

    public bool IsPaused => _isPaused;

    public void Pause()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        _isPaused = true;
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        _isPaused = false;
    }

    public void LoadMenu()
    {
        Unpause();
        Loader.Load(Loader.Scenes.MainMenu);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
