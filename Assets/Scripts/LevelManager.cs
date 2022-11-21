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

    // Start is called before the first frame update
    void Start()
    {
        played = false;
        reset = false;
        totalTime = 0f;
        totalProjectiles = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!musicPlayer.GetComponent<AudioSource>().isPlaying && played == true)
        {
            endScreen.SetActive(true);
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
}
