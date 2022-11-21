using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class NoteRainManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject noteObject;
    private BossBehavior boss;
    private LevelManager levelManager;

    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        boss = GetComponent<BossBehavior>();
        Koreographer.Instance.RegisterForEvents("NoteRain", FireEventDebugLog);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FireEventDebugLog(KoreographyEvent koreoEvent)
    {
        // Debug.Log(koreoEvent.GetIntValue());
        if(boss.ReturnCurrentAttack() == "NoteRain")
        {
            Instantiate(noteObject, new Vector3(-8.63f + (koreoEvent.GetIntValue() - 21) * (17.26f / 88), 0f, 0f), Quaternion.identity);
            levelManager.AddToTotalProjectiles();
        }
    }
}
