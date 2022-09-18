using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class ChordManager : MonoBehaviour
{
    public GameObject chord;

    // Start is called before the first frame update
    void Start()
    {
        Koreographer.Instance.RegisterForEvents("Guitar", FireChord);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FireChord(KoreographyEvent koreoEvent)
    {
        Debug.Log(koreoEvent.GetIntValue());
        Instantiate(chord, transform.position, Quaternion.identity);
    }
}
