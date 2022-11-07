using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int combo;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        combo = 0;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCombo()
    {
        combo += 1;
    }
    
    public void ResetCombo()
    {
        combo = 0;
    }

    public void AddScore()
    {
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

    public int ReturnScore()
    {
        return score;
    }
}
