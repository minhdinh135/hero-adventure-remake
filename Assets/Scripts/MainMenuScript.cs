using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadScene("DemoScene");
    }

    public void GoToSetting()
    {
        SceneManager.LoadScene("SettingScene");
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
