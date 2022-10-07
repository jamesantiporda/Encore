using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class FClefManager : MonoBehaviour
{
    public GameObject fclef;
    private BossBehavior boss;
    float yPosition;

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
            yPosition = Random.Range(-3.4f, 3.4f);
            transform.position = new Vector3(transform.position.x, -3.4f + (koreoEvent.GetIntValue() - 21) * (6.8f / 88), transform.position.z);
            Instantiate(fclef, transform.position, Quaternion.identity);
        }
    }
}
