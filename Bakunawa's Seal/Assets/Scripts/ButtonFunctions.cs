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
}
