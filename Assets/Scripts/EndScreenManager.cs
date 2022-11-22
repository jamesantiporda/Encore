using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    public TextMeshProUGUI score, combo, hits, dodged, highScoreText;
    public GameObject sRank, aRank, bRank, cRank, dRank, newText;
    public ScoreManager scoreManager;
    public LevelManager levelManager;
    public PlayerMove player;

    private float percentDodged;

    private int highScore;


    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_highscore", 0);

        percentDodged = 100f - ((float) player.ReturnHits() / (float) scoreManager.ReturnTotalCombo()) * 100f;
        score.text = "SCORE: " + scoreManager.ReturnScore();
        combo.text = "HIGHEST COMBO: " + scoreManager.ReturnHighestCombo();
        hits.text = "HIT BY: " + player.ReturnHits() + " attacks";
        dodged.text = "% DODGED: " + Math.Round(percentDodged, 2) + "%";

        if(percentDodged > 97)
        {
            sRank.SetActive(true);
        }
        else if(percentDodged > 90)
        {
            aRank.SetActive(true);
        }
        else if(percentDodged > 80)
        {
            bRank.SetActive(true);
        }
        else if(percentDodged > 70)
        {
            cRank.SetActive(true);
        }
        else
        {
            dRank.SetActive(true);
        }

        if (scoreManager.ReturnScore() > highScore)
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_highscore", scoreManager.ReturnScore());
            highScoreText.text = "HIGHSCORE: " + scoreManager.ReturnScore();
            newText.SetActive(true);
        }
        else
        {
            highScoreText.text = "HIGHSCORE: " + highScore;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
