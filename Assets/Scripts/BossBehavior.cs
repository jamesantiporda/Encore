using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField]
    private CSVReader reader;

    private float songLength;
    private float[] segmentDurations;
    private float[] segmentTimestamps;
    private float startTime, elapsedTime, attackTimer, previousAttackTime, previousSegmentTime;
    private int currentSegment;
    private string[][] segmentAttacks;
    private string[] attacks = {"Drums", "WholeNote", "NoteRain", "Chord", "NoteDrizzle", "Lunge", "FClef", "NoteBomb", "DoubleStaff", "StaffBlast"};
    private string currentAttack;
    private NoteDrizzleBehavior noteDrizzle;
    private DrumManager drums;

    private int randomizer;

    // Start is called before the first frame update
    void Start()
    {
        noteDrizzle = GameObject.FindObjectOfType<NoteDrizzleBehavior>();
        drums = GetComponent<DrumManager>();
        randomizer = 0;
        currentSegment = 0;
        startTime = Time.time;
        previousAttackTime = Time.time;
        previousSegmentTime = Time.time;
        attackTimer = 0f;

        segmentDurations = new float[reader.GetNumberOfValueRows()];
        songLength = 0;

        currentAttack = "";

        for(int i = 0; i < reader.GetNumberOfValueRows(); i++)
        {
            segmentDurations[i] = reader.GetValue("Duration_in_Seconds", i);
            songLength += segmentDurations[i];
        }

        segmentTimestamps = new float[segmentDurations.Length];
        for(int i = 0; i < segmentTimestamps.Length; i++)
        {
            if(i == 0)
            {
                segmentTimestamps[i] = 0;
            }
            else
            {
                segmentTimestamps[i] = segmentTimestamps[i - 1] + segmentDurations[i - 1];
            }
        }

        segmentAttacks = new string[segmentDurations.Length][];

        for (int i = 0; i < segmentDurations.Length; i++)
        {
            segmentAttacks[i] = AttackAssignment(i).Split(",");
        }

        currentAttack = AttackPick();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentSegment);
        attackTimer = Time.time - previousAttackTime;
        if(attackTimer >= 5.0f)
        {
            currentAttack = AttackPick();
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
        drums.ResetCount();
        randomizer = Random.Range(0, 2);
        attackTimer = 0f;
        previousAttackTime = Time.time;
        int attackPicked = Random.Range(0, segmentAttacks[currentSegment].Length);

        Debug.Log("Segment: " + currentSegment);
        Debug.Log("Attack: " + segmentAttacks[currentSegment][attackPicked]);

        if(segmentAttacks[currentSegment][attackPicked] == "NoteDrizzle")
        {
            noteDrizzle.InitializeAttack();
        }

        return segmentAttacks[currentSegment][attackPicked];
    }

    string AttackAssignment(int segment)
    {
        string s = "";

        float uniquePitches = reader.GetValue("Number_of_Pitches", segment);
        float range = reader.GetValue("Range", segment);
        if (uniquePitches >= 30 && range >= 50)
        {
            if (s == "")
                s += "NoteRain";
            else 
                s += ",NoteRain";
        }

        float chordDuration = reader.GetValue("Chord_Duration", segment);
        float majorMinorThirds = reader.GetValue("Vertical_Minor_Third_Prevalence", segment) + reader.GetValue("Vertical_Major_Third_Prevalence", segment);
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

        float repeatedNotes = reader.GetValue("Repeated_Notes", segment);
        float staccato = reader.GetValue("Amount_of_Staccato", segment);
        if(repeatedNotes >= 0.6 || staccato >= 0.2)
        {
            if (s == "")
                s += "FClef";
            else
                s += ",FClef";
        }

        float unpitchedNotes = reader.GetValue("Unpitched_Percussion_Instrument_Prevalence", segment);
        float simultaneousPitches = reader.GetValue("Average_Number_of_Simultaneous_Pitches", segment);
        if (unpitchedNotes > 0.0 || majorMinorThirds >= 0.2)
        {
            if (s == "")
                s += "NoteBomb";
            else
                s += ",NoteBomb";
        }

        float violinPrevalence = reader.GetValue("Violin_Prevalence", segment);
        float chromatic = reader.GetValue("Chromatic_Motion", segment);
        float stepwise = reader.GetValue("Stepwise_Motion", segment);
        if (violinPrevalence > 0 || chromatic >= 0.40 || (stepwise >= 0.40 && range >= 30))
        {
            if (s == "")
                s += "DoubleStaff";
            else
                s += ",DoubleStaff";
        }

        if (simultaneousPitches >= 5 && chordDuration >= 1)
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
}
