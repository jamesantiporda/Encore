using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class FClefManager : MonoBehaviour
{
    public GameObject fclef;
    private BossBehavior boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<BossBehavior>();
        Koreographer.Instance.RegisterForEvents("Piano", FireFClefs);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FireFClefs(KoreographyEvent koreoEvent)
    {
        if (boss.ReturnCurrentAttack() == "FClef")
        {
            Instantiate(fclef, transform.position, Quaternion.identity);
        }
    }
}
