using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class NoteDrizzleBehavior : MonoBehaviour
{
    private GameObject noteDrizzle;
    public GameObject zone;
    public GameObject blackBars;
    PlayerMove target;
    public GameObject noteObject;
    private string newNote;
    private BossBehavior boss;
    private LevelManager levelManager;
    private AudioSource onSFX;
    private bool isActive;

    private void Awake()
    {
        onSFX = GetComponent<AudioSource>();
        target = GameObject.FindObjectOfType<PlayerMove>();
    }

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        noteDrizzle = this.GetComponent<GameObject>();
        boss = GameObject.FindObjectOfType<BossBehavior>();
        Koreographer.Instance.RegisterForEvents("NoteDrizzle", FireNoteDrizzle);
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FireNoteDrizzle(KoreographyEvent koreoEvent)
    {
        if(boss.ReturnCurrentAttack() == "NoteDrizzle")
        {
            Debug.Log(koreoEvent.GetTextValue());
            newNote = koreoEvent.GetTextValue();
            if (newNote == "C" || newNote == "C#/Db")
            {
                Instantiate(noteObject, transform.GetChild(0).transform.position, Quaternion.identity);
                levelManager.AddToTotalProjectiles();
            }
            if (newNote == "D" || newNote == "D#/Eb")
            {
                Instantiate(noteObject, transform.GetChild(1).transform.position, Quaternion.identity);
                levelManager.AddToTotalProjectiles();
            }
            if (newNote == "E")
            {
                Instantiate(noteObject, transform.GetChild(2).transform.position, Quaternion.identity);
                levelManager.AddToTotalProjectiles();
            }
            if (newNote == "F" || newNote == "F#/Gb")
            {
                Instantiate(noteObject, transform.GetChild(3).transform.position, Quaternion.identity);
                levelManager.AddToTotalProjectiles();
            }
            if (newNote == "G" || newNote == "G#/Ab")
            {
                Instantiate(noteObject, transform.GetChild(4).transform.position, Quaternion.identity);
                levelManager.AddToTotalProjectiles();
            }
            if (newNote == "A" || newNote == "A#/Bb")
            {
                Instantiate(noteObject, transform.GetChild(5).transform.position, Quaternion.identity);
                levelManager.AddToTotalProjectiles();
            }
            if (newNote == "B")
            {
                Instantiate(noteObject, transform.GetChild(6).transform.position, Quaternion.identity);
                levelManager.AddToTotalProjectiles();
            }
        }
    }

    public void InitializeAttack()
    {
        isActive = true;
        
        zone.SetActive(true);
        transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
    }

    public void DeactivateAttack()
    {
        zone.SetActive(false);
        if (isActive == true)
        {
            onSFX.Play();
            isActive = false;
        }
    }

    public void TurnOnWithBlack()
    {
        onSFX.Play();
        zone.SetActive(true);
        blackBars.SetActive(true);
        transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
    }
}
