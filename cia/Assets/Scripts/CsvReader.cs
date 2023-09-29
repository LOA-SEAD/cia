using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class CsvReader : MonoBehaviour
{
    public string[] data_sentences;
    public string[] data_balloons;
    public string[] data_expressions;
    public int line;
    [SerializeField] private TextAsset _csvFile;
    public List<string> eachLine;
    public string data_string;

    // Start is called before the first frame update
    void Awake()
    {
        Read();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Read()
    {
        int id = PlayerPrefs.GetInt("NarrativaId", 0);

        data_string = _csvFile.text;
        eachLine = new List<string>();
        eachLine.AddRange(data_string.Split("\n"[0]));
        // Debug.Log(eachLine[id]);
        data_sentences = eachLine[id].Split(';');
        data_balloons = eachLine[id+1].Split(';');
        data_expressions = eachLine[id+2].Split(';');
        //Debug.Log(data_values[3]);
        //Debug.Log(data_values[1]);

    }
}
