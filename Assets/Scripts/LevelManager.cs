using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private bool reset, control;
    float totalTime;
    public GameObject musicPlayer;
    bool played;
    public GameObject endScreen;
    int totalProjectiles;

    public GameObject pauseScreen;
    private bool gameIsPaused, pause, gameEnded;

    // Start is called before the first frame update
    void Start()
    {
        gameIsPaused = false;
        pauseScreen.SetActive(gameIsPaused);
        played = false;
        reset = false;
        totalTime = 0f;
        totalProjectiles = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!musicPlayer.GetComponent<AudioSource>().isPlaying && played == true && !gameIsPaused)
        {
            endScreen.SetActive(true);
            gameEnded = true;
        }

        totalTime += Time.deltaTime;

        if(played == false && totalTime >= 5f)
        {
            played = true;
            musicPlayer.GetComponent<AudioSource>().Play();
        }

        // Resetting Level
        reset = Input.GetKeyDown(KeyCode.R);
        control = Input.GetKey(KeyCode.LeftControl);

        if (reset && control)
        {
            ResetLevel();
        }

        // Pausing Level
        pause = Input.GetKeyDown(KeyCode.Escape);
        if(pause && !gameIsPaused && !gameEnded)
        {
            PauseGame();
        }
        else if (pause && gameIsPaused)
        {
            ResumeGame();
        }

        //Debug.Log(totalTime);
    }

    public void ResetLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddToTotalProjectiles()
    {
        totalProjectiles++;
    }

    public int ReturnTotalProjectiles()
    {
        return totalProjectiles;
    }

    public void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;
        musicPlayer.GetComponent<AudioSource>().Pause();
        pauseScreen.SetActive(gameIsPaused);
    }

    public void ResumeGame()
    {
        gameIsPaused = false;
        musicPlayer.GetComponent<AudioSource>().Play();
        Time.timeScale = 1.0f;
        pauseScreen.SetActive(gameIsPaused);
    }

    public void LevelSelect()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("LevelSelect");
    }
}
