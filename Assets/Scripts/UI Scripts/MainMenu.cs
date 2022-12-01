using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void PlayFirstLevel()
    {
        SceneManager.LoadScene("Twinkle");
    }

    public void PlaySecondLevel()
    {
        SceneManager.LoadScene("LaCampanella");
    }

    public void PlayThirdLevel()
    {
        SceneManager.LoadScene("WinterWind");
    }

    public void PlayFourthLevel()
    {
        SceneManager.LoadScene("Malenia");
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("I am a quitter");
    }
}
