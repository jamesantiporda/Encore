using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class WholeNoteManager : MonoBehaviour
{
    public GameObject wholeNote;
    private BossBehavior boss;

    // Start is called before the first frame update
    void Start()
    {
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
        }
        else
        {
            if(boss.ReturnCurrentAttack() != "NoteBomb" && boss.ReturnCurrentAttack() != "DoubleStaff" && boss.ReturnCurrentAttack() != "Chord")
            {
                Instantiate(wholeNote, transform.position, Quaternion.identity);
            }
        }
    }
}
