using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingScript : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
