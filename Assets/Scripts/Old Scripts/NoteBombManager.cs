using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class NoteBombManager : MonoBehaviour
{
    public GameObject noteBomb;
    private BossBehavior boss;
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        boss = GetComponent<BossBehavior>();
        Koreographer.Instance.RegisterForEvents("Chords", FireChord);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FireChord(KoreographyEvent koreoEvent)
    {
        if (boss.ReturnCurrentAttack() == "NoteBomb")
        {
            Instantiate(noteBomb, transform.position, Quaternion.identity);
            levelManager.AddToTotalProjectiles();
        }
    }
}
