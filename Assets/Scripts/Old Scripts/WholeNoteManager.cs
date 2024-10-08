using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class WholeNoteManager : MonoBehaviour
{
    public GameObject wholeNote;
    private BossBehavior boss;
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        boss = GetComponent<BossBehavior>();
        Koreographer.Instance.RegisterForEvents("Piano", FireWholeNotes);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FireWholeNotes(KoreographyEvent koreoEvent)
    {
        if (boss.ReturnCurrentAttack() == "")
        {
            Instantiate(wholeNote, transform.position, Quaternion.identity);
            levelManager.AddToTotalProjectiles();
        }
        else
        {
            if(boss.ReturnCurrentAttack() != "NoteBomb" && boss.ReturnCurrentAttack() != "DoubleStaff" && boss.ReturnCurrentAttack() != "Chord" && boss.ReturnCurrentAttack() != "FClef")
            {
                Instantiate(wholeNote, transform.position, Quaternion.identity);
                levelManager.AddToTotalProjectiles();
            }
        }
    }
}
