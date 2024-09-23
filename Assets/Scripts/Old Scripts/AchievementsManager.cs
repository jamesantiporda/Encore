using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    private int finishedLevels = 0;
    private bool tenMill = false, noHit = false, noShoot = false, isOpen = false;  

    public GameObject levelsAchievement, tenMillAchievement, noHitAchievement, noShootAchievement;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("finishedLevels"))
        {
            PlayerPrefs.SetInt("finishedLevels", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementFinishedLevels()
    {
        PlayerPrefs.SetInt("finishedLevels", PlayerPrefs.GetInt("finishedLevels") + 1);
    }

    public void SetTenMillTrue()
    {
        tenMill = true;
        PlayerPrefs.SetInt("tenMill", 1);
    }

    public void SetNoHitTrue() 
    { 
        noHit = true;
        PlayerPrefs.SetInt("noHit", 1);
    }

    public void SetNoShootTrue()
    {
        noShoot = true;
        PlayerPrefs.SetInt("noShoot", 1);
    }

    public void AchievementsOpen()
    {
        isOpen = true;
        if(PlayerPrefs.GetInt("finishedLevels") >= 3)
        {
            levelsAchievement.SetActive(true);
        }

        if(PlayerPrefs.HasKey("tenMill"))
        {
            if(PlayerPrefs.GetInt("tenMill") == 1)
            {
                tenMillAchievement.SetActive(true);
            }
        }

        if(PlayerPrefs.HasKey("noHit"))
        {
            if(PlayerPrefs.GetInt("noHit") == 1)
            {
                noHitAchievement.SetActive(true);
            }
        }

        if(PlayerPrefs.HasKey("noShoot"))
        {
            noShootAchievement.SetActive(true);
        }
    }

    public void AchievementsClose()
    {
        isOpen = false;
    }
}
