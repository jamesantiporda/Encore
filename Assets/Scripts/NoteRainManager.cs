using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class NoteRainManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject noteObject;
    private BossBehavior boss;

    void Start()
    {
        boss = GetComponent<BossBehavior>();
        Koreographer.Instance.RegisterForEvents("NoteRain", FireEventDebugLog);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FireEventDebugLog(KoreographyEvent koreoEvent)
    {
        // Debug.Log(koreoEvent.GetIntValue());
        if(boss.ReturnCurrentAttack() == "NoteRain")
        {
            Instantiate(noteObject, new Vector3(-10.4f + (koreoEvent.GetIntValue() - 21) * (20.8f / 88), 0f, 0f), Quaternion.identity);
        }
    }
}
