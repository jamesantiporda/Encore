using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int combo;
    int highestCombo;
    int score;
    int totalCombo;

    private AudioSource hitSFX;

    // Start is called before the first frame update
    void Start()
    {
        hitSFX = GetComponent<AudioSource>();
        combo = 0;
        score = 0;
        highestCombo = 0;
        totalCombo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(combo > highestCombo)
        {
            highestCombo = combo;
        }
    }

    public void AddCombo()
    {
        combo += 1;
        totalCombo += 1;
    }
    
    public void ResetCombo()
    {
        combo = 0;
    }

    public void AddScore()
    {
        hitSFX.Play();
        if(combo > 1)
        {
            score += combo * 100;
        }
        else if(combo <= 1)
        {
            score += 100;
        }
    }

    public int ReturnCombo()
    {
        return combo;
    }

    public int ReturnHighestCombo()
    {
        return highestCombo;
    }

    public int ReturnScore()
    {
        return score;
    }

    public int ReturnTotalCombo()
    {
        return totalCombo;
    }
}
