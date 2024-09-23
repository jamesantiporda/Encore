using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class BossBehaviorData : ScriptableObject
{
    public CSVData csvDataRightHand;
    public CSVData csvDataLeftHand;

    public string[] segmentAttacks; // Attack Pool for each segment of music
    public float[] segmentDurations; // Duration of each segment of music in seconds
    public float[] segmentPitches; // Mean Pitches of each segment
    public bool[] powerUpSegments; // Segments where powerups should spawn

    public float songLength = 0f;

    public void GenerateSegmentAttacksFromCSV()
    {
        segmentDurations = new float[csvDataRightHand.GetNumberOfValueRows()];

        songLength = 0f;

        //Debug.Log("works");

        //Debug.Log(csvDataRightHand.GetValue("Duration_in_Seconds", 4));

        // Create Array of Durations of each Segment
        for (int i = 0; i < csvDataRightHand.GetNumberOfValueRows(); i++)
        {
            Debug.Log("Segment " + i +": " + csvDataRightHand.GetValue("Duration_in_Seconds", i));
            segmentDurations[i] = csvDataRightHand.GetValue("Duration_in_Seconds", i);
            songLength += segmentDurations[i];
        }

        // Create Array of Segments where Powerups spawn
        powerUpSegments = new bool[csvDataRightHand.GetNumberOfValueRows()];

        for (int i = 0; i < csvDataRightHand.GetNumberOfValueRows(); i++)
        {
            float noteDensity = csvDataRightHand.GetValue("Note_Density", i);
            float noteDensity1 = csvDataLeftHand.GetValue("Note_Density", i);
            float timeBetweenAttacks = csvDataRightHand.GetValue("Average_Time_Between_Attacks", i);
            float timeBetweenAttacks1 = csvDataLeftHand.GetValue("Average_Time_Between_Attacks", i);

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
        for (int i = 0; i < segmentPitches.Length; i++)
        {

            segmentPitches[i] = (csvDataRightHand.GetValue("Mean_Pitch", i) + csvDataLeftHand.GetValue("Mean_Pitch", i)) / 2;

        }

        //Debug.Log("works");

        // Create Array of Attack Pools
        segmentAttacks = new string[segmentDurations.Length];

        string attackString = "";
        for (int i = 0; i < segmentDurations.Length; i++)
        {
            attackString = AttackAssignment(i, csvDataRightHand) + "," + AttackAssignment(i, csvDataLeftHand);
            segmentAttacks[i] = attackString;
        }

        //Debug.Log("works again");
    }

    string AttackAssignment(int segment, CSVData csv)
    {
        string s = "";

        float uniquePitches = csvDataRightHand.GetValue("Number_of_Pitches", segment) + csvDataLeftHand.GetValue("Number_of_Pitches", segment); ;
        float range = csvDataRightHand.GetValue("Range", segment) + csvDataLeftHand.GetValue("Range", segment);
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

        if (uniquePitches >= 8 && uniquePitches <= 20)
        {
            if (s == "")
                s += "NoteDrizzle";
            else
                s += ",NoteDrizzle";
        }

        float repeatedNotes = csv.GetValue("Repeated_Notes", segment);
        float staccato = csv.GetValue("Amount_of_Staccato", segment);
        if (repeatedNotes >= 0.6 || staccato >= 0.2)
        {
            if (staccato >= 0.2)
            {
                Debug.Log("STAC");
            }
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
}
