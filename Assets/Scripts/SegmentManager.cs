using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;


public class SegmentManager : MonoBehaviour
{
    private BossBehavior boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = this.GetComponent<BossBehavior>();
        Koreographer.Instance.RegisterForEvents("timestamps", FireSegmentUpdate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FireSegmentUpdate(KoreographyEvent koreoEvent)
    {
        boss.IncrementSegment();
    }
}

