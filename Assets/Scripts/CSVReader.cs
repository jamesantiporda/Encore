using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public TextAsset features;
    private string[] headers;
    private string[][] valuesString;
    private float[][] valuesRows;
    private int numberOfValueRows;
    private int numberOfColumns;
    private List<float> values = new List<float>();

    void Awake()
    {
        string[] rows = features.text.Split(new char[] { '\n' });
        numberOfValueRows = rows.Length - 2;
        headers = rows[0].Split(new char[] { ',' });
        numberOfColumns = headers.Length;
        Debug.Log("Real Rows: " + rows.Length);
        Debug.Log("no of columns " + headers.Length);
        valuesRows = new float[numberOfValueRows][];
        valuesString = new string[numberOfValueRows][];
        Debug.Log("no of rows" + numberOfValueRows);

        for (int i = 0; i < numberOfValueRows; i++)
        {
            valuesString[i] = rows[i + 1].Split(new char[] { ',' });
        }

        float[] dummyFloatArray;

        for (int j = 0; j < numberOfValueRows; j++)
        {
            dummyFloatArray = new float[numberOfColumns];
            for (int k = 0; k < numberOfColumns; k++)
            {
                dummyFloatArray[k] = float.Parse(valuesString[j][k]);
            }
            valuesRows[j] = dummyFloatArray;
        }

        /*
        foreach(string v in valuesString)
        {
            values.Add(float.Parse(v));
        }
        */

        Debug.Log(GetValue("Duration_in_Seconds", 4));
    }

    void Start()
    {
        
    }

    public float GetValue(string data, int section)
    {
        if (section > numberOfValueRows - 1 || section < 0)
        {
            Debug.Log("Index Out of Range! There is no section with number: " + section);
            return 0;
        }

        int i = 0;
        foreach(string header in headers)
        {
            if(header == data)
            {
                break;
            }
            i++;
        }
        return valuesRows[section][i];
    }

    public int GetNumberOfValueRows()
    {
        return numberOfValueRows;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
