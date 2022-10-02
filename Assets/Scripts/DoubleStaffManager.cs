using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class DoubleStaffManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject doubleStaff;
    private BossBehavior boss;


    void Start()
    {
        boss = GetComponent<BossBehavior>();
        Koreographer.Instance.RegisterForEvents("Piano", FireDoubleStaff);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FireDoubleStaff(KoreographyEvent koreoEvent)
    {
        if (boss.ReturnCurrentAttack() == "DoubleStaff")
        {
            // Debug.Log(koreoEvent.GetIntValue());
            Instantiate(doubleStaff, new Vector3(0f, -2.5f + (koreoEvent.GetIntValue() - 21) * (5.0f / 88), 0f), Quaternion.identity);
        }
    }
}
