using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private bool reset, control;
    float totalTime, songLength;
    public GameObject musicPlayer;
    bool played, deathScreen;
    public GameObject endScreen;
    int totalProjectiles;

    public GameObject pauseScreen;
    public GameObject UI;
    public GameObject death;
    private bool gameIsPaused, pause, gameEnded;

    private PlayerMove player;
    public NoteDrizzleBehavior noteDrizzle;
    public BossBehavior boss;

    // Start is called before the first frame update
    void Start()
    {
        deathScreen = false;
        player = GameObject.FindObjectOfType<PlayerMove>();
        gameIsPaused = false;
        pauseScreen.SetActive(gameIsPaused);
        played = false;
        reset = false;
        totalTime = 0f;
        songLength = boss.ReturnSongLength();
        totalProjectiles = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!musicPlayer.GetComponent<AudioSource>().isPlaying && played == true && !gameIsPaused && player.PlayerIsAlive() && totalTime >= (songLength + 5))
        {
            UI.SetActive(false);
            endScreen.SetActive(true);
            gameEnded = true;
            boss.DisableBoss();
        }

        if(!player.PlayerIsAlive() && !deathScreen)
        {
            UI.SetActive(false);
            deathScreen = true;
            noteDrizzle.TurnOnWithBlack();
            boss.DisableBoss();
            StartCoroutine(waiter());
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
        pause = Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P);
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
        if(played)
        {
            musicPlayer.GetComponent<AudioSource>().Pause();
        }
        pauseScreen.SetActive(gameIsPaused);
        UI.SetActive(false);
    }

    public void ResumeGame()
    {
        gameIsPaused = false;
        if(played)
        {
            musicPlayer.GetComponent<AudioSource>().Play();
        }
        Time.timeScale = 1.0f;
        pauseScreen.SetActive(gameIsPaused);
        UI.SetActive(true);
    }

    public void LevelSelect()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("LevelSelect");
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(3f);
        death.SetActive(true);

    }
}
