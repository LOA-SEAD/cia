using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldController : MonoBehaviour
{
    [SerializeField] private TextAsset _csvFilePhrases;
    [SerializeField] private TMP_Text phraseTextBox;
    private List<string> eachLine;
    public string data_string;
    public string input;
    public TMP_InputField inputField;
    public string[] eachPhrase;
    public List<string> wordsRead = new List<string>();
    public AudioClip changePhraseSound;
    public AudioClip rightAnswer;
    private AudioManager audioManager;
    private ObjectivesController objController;
    private List<bool> checkPositions = new List<bool>();

    private int phraseId=0;

    // Start is called before the first frame update
    private void Start()
    {
        // audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        Read();
        phraseTextBox.text = eachPhrase[phraseId];
        objController = GameObject.Find("ObjetivosBG").GetComponent<ObjectivesController>();
    }

    public void ReadStringInput()
    {
        input = inputField.text;
        inputField.text = "";
        ValidateWords();
    }

    public void NextCase(){
        phraseId++;
        if (phraseId <= eachPhrase.Length) {
            phraseTextBox.text = eachPhrase[phraseId];
            audioManager.PlaySFX(changePhraseSound);
        }
        else
        {
            phraseId--;
        }
    }

    public void BackCase()
    {
        phraseId--;
        if (phraseId >= 0) { 
            phraseTextBox.text = eachPhrase[phraseId];
            audioManager.PlaySFX(changePhraseSound);
        }
        else
        {
            phraseId++;
        }
    }

    public void PassWords(List<string> w)
    {
        wordsRead = new List<string>(w);
        foreach(string s in wordsRead)
        {
            checkPositions.Add(false);
        }

        Debug.Log(wordsRead[0]);
    }

    void Read()
    {

        data_string = _csvFilePhrases.text;
        eachLine = new List<string>();
        eachLine.AddRange(data_string.Split("\n"[0]));
        eachPhrase = eachLine[PlayerPrefs.GetInt("LoadCaseId", 0)].Split(';');
        //casewords = eachLine[PlayerPrefs.GetInt("LoadCaseId", 0)].Split(';');

    }

    public void ValidateWords()
    {
        if (wordsRead.Contains(input))
        {
            int pos = wordsRead.FindIndex(str => str.Contains(input));
            if (checkPositions[pos] == false)
            {
                //wordsRead[pos] = "e5ef1a3s2de87rf0SCwfBTHYwefedse578899";
                objController.CountObjectivePhrase();
                string[] updateString = eachPhrase[pos].Split('@');
                eachPhrase[pos] = updateString[0] + input + updateString[1];
                phraseTextBox.text = eachPhrase[phraseId];
                checkPositions[pos] = true;
                audioManager.RightAnswer();
                
            
                }
            
        }
        audioManager.WrongAnswer();
    }
}
