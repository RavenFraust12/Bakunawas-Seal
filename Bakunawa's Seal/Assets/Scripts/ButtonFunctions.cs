using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void MainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void PauseButton()
    {
        Time.timeScale = 0f;
    }

    public void ResumeButton()
    {
        Time.timeScale = 1f;
    }

    public void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
