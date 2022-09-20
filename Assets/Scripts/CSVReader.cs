using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public TextAsset features;
    private string[] headers;
    private string[] valuesString;
    private List<float> values = new List<float>();

    void Start()
    {
        string[] rows = features.text.Split(new char[] {'\n'});

        headers = rows[0].Split(new char[] {','});
        valuesString = rows[1].Split(new char[] {','});

        foreach(string v in valuesString)
        {
            values.Add(float.Parse(v));
        }

        Debug.Log(headers[0] + " = " + values[0]);
        Debug.Log(GetValue("Repeated_Notes"));
    }

    float GetValue(string data)
    {
        int i = 0;
        foreach(string header in headers)
        {
            if(header == data)
            {
                break;
            }
            i++;
        }
        return values[i];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
