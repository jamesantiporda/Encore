using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class DrumManager : MonoBehaviour
{
    public GameObject drum;
    private BossBehavior boss;
    private float yPosition;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        boss = GetComponent<BossBehavior>();
        Koreographer.Instance.RegisterForEvents("beats", FireWholeNotes);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FireWholeNotes(KoreographyEvent koreoEvent)
    {
        if (count < 4)
        {
            if (boss.ReturnCurrentAttack() == "")
            {
                yPosition = Random.Range(-4.22f, 4.22f);
                Instantiate(drum, new Vector3(transform.position.x, yPosition, transform.position.z), Quaternion.identity);
                count++;
            }
            else
            {
                if (boss.ReturnRandomizer() == 0)
                {
                    yPosition = Random.Range(-4.22f, 4.22f);
                    Instantiate(drum, new Vector3(transform.position.x, yPosition, transform.position.z), Quaternion.identity);
                    count++;
                }
            }
        }
    }

    public void ResetCount()
    {
        count = 0;
    }
}
