using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class ChordManager : MonoBehaviour
{
    public GameObject chord;
    private BossBehavior boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<BossBehavior>();
        Koreographer.Instance.RegisterForEvents("Chords", FireChord);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FireChord(KoreographyEvent koreoEvent)
    {
        if (boss.ReturnCurrentAttack() == "Chord")
        {
            Instantiate(chord, transform.position, Quaternion.identity);
        }
    }
}
