using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private bool reset;

    // Start is called before the first frame update
    void Start()
    {
        reset = false;
    }

    // Update is called once per frame
    void Update()
    {
        reset = Input.GetKeyDown(KeyCode.R);

        if (reset)
        {
            ResetLevel();
        }
    }

    public void ResetLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
