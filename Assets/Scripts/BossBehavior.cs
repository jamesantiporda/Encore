using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class BossBehavior : MonoBehaviour
{
    [SerializeField]
    private CSVReader reader;
    [SerializeField]
    private CSVReader reader1;

    private Animator animator;

    private float songLength;
    private float meanPitch;
    private float[] segmentDurations;
    private float[] segmentPitches;
    private float startTime, elapsedTime, attackTimer, previousAttackTime, previousSegmentTime;
    private int currentSegment;
    private string[][] segmentAttacks;
    private string[] attacks = {"Drums", "WholeNote", "NoteRain", "Chord", "NoteDrizzle", "Lunge", "FClef", "NoteBomb", "DoubleStaff", "StaffBlast"};
    private string currentAttack, currentAttack1;
    private NoteDrizzleBehavior noteDrizzle;
    private DrumManager drums;
    private PowerUpManager powerUpManager;
    private AudioSource hitSFX;
    private Vector3 oldPosition;
    private Vector3 newPosition;
    private float t = 0;
    public float speed = 0.1f;
    private bool moving = false;

    private float playingTime = 0.5f;
    private float lastPlayTimer = 0.0f;
    private bool playing = false;

    private bool ended = false;

    private int randomizer;

    float yPosition;

    private bool[] powerUpSegments;

    float noteDensity, timeBetweenAttacks, noteDensity1, timeBetweenAttacks1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        Koreographer.Instance.RegisterForEvents("NoteRain", Playing);
        Koreographer.Instance.RegisterForEvents("Chords", Playing);
        Koreographer.Instance.RegisterForEvents("NoteDrizzle", Playing);
        Koreographer.Instance.RegisterForEvents("Piano", Playing);
        oldPosition = transform.position;

        noteDrizzle = GameObject.FindObjectOfType<NoteDrizzleBehavior>();
        powerUpManager = GameObject.FindObjectOfType<PowerUpManager>();
        drums = GetComponent<DrumManager>();
        hitSFX = GetComponent<AudioSource>();

        randomizer = 0;
        currentSegment = 0;
        startTime = Time.time;
        previousAttackTime = Time.time;
        previousSegmentTime = Time.time;
        attackTimer = 0f;

        segmentDurations = new float[reader.GetNumberOfValueRows()];
        songLength = 0;

        currentAttack = "";
        currentAttack1 = "";

        // Create Array of Durations of each Segment
        for (int i = 0; i < reader.GetNumberOfValueRows(); i++)
        {
            segmentDurations[i] = reader.GetValue("Duration_in_Seconds", i);
            songLength += segmentDurations[i];
        }


        // Create Array of Segments where Powerups spawn
        powerUpSegments = new bool[reader.GetNumberOfValueRows()];

        for (int i = 0; i < reader.GetNumberOfValueRows(); i++)
        {
            noteDensity = reader.GetValue("Note_Density", i);
            noteDensity1 = reader1.GetValue("Note_Density", i);
            timeBetweenAttacks = reader.GetValue("Average_Time_Between_Attacks", i);
            timeBetweenAttacks1 = reader1.GetValue("Average_Time_Between_Attacks", i);

            if (noteDensity >= 8 || noteDensity1 >= 8 || timeBetweenAttacks <= 0.150 || timeBetweenAttacks1 <= 0.150)
            {
                powerUpSegments[i] = true;
            }
            else
            {
                powerUpSegments[i] = false;
            }
        }

        // Create Array of Mean Pitches of each Segment
        segmentPitches = new float[segmentDurations.Length];
        for(int i = 0; i < segmentPitches.Length; i++)
        {
            
             segmentPitches[i] = (reader.GetValue("Mean_Pitch", i) + reader1.GetValue("Mean_Pitch", i))/2;
            
        }

        // Add Attacks to the attack pool of each segment
        segmentAttacks = new string[segmentDurations.Length][];

        for (int i = 0; i < segmentDurations.Length; i++)
        {
            segmentAttacks[i] = AttackAssignment(i, reader).Split(",");
            segmentAttacks[i] = AttackAssignment(i, reader1).Split(",");
        }

        currentAttack = AttackPick();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentSegment);
        
        if(lastPlayTimer >= playingTime)
        {
            playing = false;
            animator.SetBool("IsPlaying", playing);
        }
        else
        {
            lastPlayTimer += Time.deltaTime;
        }

        attackTimer = Time.time - previousAttackTime;
        if(attackTimer >= 5.0f && !ended)
        {
            currentAttack = AttackPick();
        }

        meanPitch = segmentPitches[currentSegment];
        newPosition = new Vector3(transform.position.x, -2.0f + (meanPitch - 21) * (5.0f / 88), 0);
        if (moving)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(oldPosition.y, newPosition.y, t), 0);
            t += speed * Time.deltaTime;
            if (t >= 1.0f)
            {
                moving = false;
            }
        }
    }

    void FixedUpdate()
    {
        /*
        elapsedTime = Time.time - startTime;
        for (int i = 0; i < segmentTimestamps.Length; i++)
        {
            if (elapsedTime >= segmentTimestamps[i])
            {
                if (i != 0)
                {
                    segmentTimestamps[i - 1] = 10000f;
                }
                currentSegment = i;
                Debug.Log("Segment: " + i);
            }
        }
        */

        /*
        elapsedTime = Time.fixedTime - previousSegmentTime;
        if (elapsedTime >= segmentDurations[currentSegment] && currentSegment != segmentDurations.Length - 1)
        {
            currentSegment++;
            elapsedTime = 0;
            previousSegmentTime = Time.fixedTime;
            AttackPick();
        }
        */
        
    }

    public void IncrementSegment()
    {
        if(currentSegment != segmentDurations.Length - 1)
        {
            currentSegment++;
            currentAttack = AttackPick();
        }
    }

    string AttackPick()
    {
        t = 0;
        moving = true;
        oldPosition = transform.position;
        drums.ResetCount();
        randomizer = Random.Range(0, 2);
        attackTimer = 0f;
        previousAttackTime = Time.time;
        int attackPicked = Random.Range(0, segmentAttacks[currentSegment].Length);

        Debug.Log("Segment: " + currentSegment);
        Debug.Log("Attack: " + segmentAttacks[currentSegment][attackPicked]);

        noteDrizzle.DeactivateAttack();

        if(segmentAttacks[currentSegment][attackPicked] == "NoteDrizzle")
        {
            noteDrizzle.InitializeAttack();
        }

        if(segmentAttacks[currentSegment][attackPicked] == "FClef")
        {
            yPosition = Random.Range(-3.4f, 3.4f);
            //transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
        }

        if (powerUpSegments[currentSegment])
        {
            randomizer = Random.Range(0, 5);

            if(randomizer == 1)
            {
                powerUpManager.SpawnPowerUp();
            }
        }

        return segmentAttacks[currentSegment][attackPicked];
    }

    string AttackAssignment(int segment, CSVReader csv)
    {
        string s = "";

        float uniquePitches = csv.GetValue("Number_of_Pitches", segment);
        float range = csv.GetValue("Range", segment);
        if (uniquePitches >= 30 && range >= 50)
        {
            if (s == "")
                s += "NoteRain";
            else 
                s += ",NoteRain";
        }

        float chordDuration = csv.GetValue("Chord_Duration", segment);
        float majorMinorThirds = csv.GetValue("Vertical_Minor_Third_Prevalence", segment) + csv.GetValue("Vertical_Major_Third_Prevalence", segment);
        if (majorMinorThirds >= 0.2)
        {
            if (s == "")
                s += "Chord";
            else
                s += ",Chord";
        }

        if (uniquePitches >= 8 && uniquePitches <= 30)
        {
            if (s == "")
                s += "NoteDrizzle";
            else
                s += ",NoteDrizzle";
        }

        if (chordDuration >= 1)
        {
            if (s == "")
                s += "Lunge";
            else
                s += ",Lunge";
        }

        float repeatedNotes = csv.GetValue("Repeated_Notes", segment);
        float staccato = csv.GetValue("Amount_of_Staccato", segment);
        if(repeatedNotes >= 0.6 || staccato >= 0.2)
        {
            if (s == "")
                s += "FClef";
            else
                s += ",FClef";
        }

        float unpitchedNotes = csv.GetValue("Unpitched_Percussion_Instrument_Prevalence", segment);
        float simultaneousPitches = csv.GetValue("Average_Number_of_Simultaneous_Pitches", segment);
        if (unpitchedNotes > 0.0 || majorMinorThirds >= 0.2)
        {
            if (s == "")
                s += "NoteBomb";
            else
                s += ",NoteBomb";
        }

        float violinPrevalence = csv.GetValue("Violin_Prevalence", segment);
        float chromatic = csv.GetValue("Chromatic_Motion", segment);
        float stepwise = csv.GetValue("Stepwise_Motion", segment);
        if (violinPrevalence > 0 || chromatic >= 0.40 || stepwise >= 0.40) // && range >= 30))
        {
            if (s == "")
                s += "DoubleStaff";
            else
                s += ",DoubleStaff";
        }

        if ((majorMinorThirds >= 0.2 || simultaneousPitches >= 5) && chordDuration >= 1)
        {
            if (s == "")
                s += "StaffBlast";
            else
                s += ",StaffBlast";
        }

        return s;
    }

    public string ReturnCurrentAttack()
    {
        return currentAttack;
    }

    public int ReturnRandomizer()
    {
        return randomizer;
    }

    void Playing(KoreographyEvent koreoEvent)
    {
        playing = true;
        animator.SetBool("IsPlaying", playing);
        lastPlayTimer = 0f;
    }

    public void DisableBoss()
    {
        ended = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            hitSFX.Play();
            animator.SetTrigger("Hit");
        }
    }
}
