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
    private float startTime, elapsedTime;
    private int currentSegment;

    // Start is called before the first frame update
    void Start()
    {
        currentSegment = 0;
        startTime = Time.time;

        segmentDurations = new float[reader.GetNumberOfValueRows()];
        songLength = 0;

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
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentSegment);
    }

    void FixedUpdate()
    {
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
            }
        }
    }
}
