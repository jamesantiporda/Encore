using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class StaffBlastManager : MonoBehaviour
{
    public GameObject staffBlast;
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
        if (boss.ReturnCurrentAttack() == "StaffBlast" || boss.ReturnCurrentAttack() == "Lunge")
        {
            Instantiate(staffBlast, transform.position, Quaternion.identity);
        }
    }
}
