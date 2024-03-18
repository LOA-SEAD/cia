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
    public List<bool> checkPositions = new List<bool>();
    private int ultimoCaso = 2;
    [SerializeField] GameObject grid;
    [SerializeField] GameObject aviso;
    [SerializeField] GameObject powerUpCanvas;
    [SerializeField] GameObject powerUpLButton;
    [SerializeField] TMP_Text detalhesCaso;
    public int countErrors = 0;
    public int countErrorsLast = 0;
    [SerializeField] GameObject avisoFree;
    [SerializeField] GameObject avisoTutorial;
    [SerializeField] private GameObject canvas;

    int contpw = 0;

    public int phraseId=0;

    // Start is called before the first frame update
    private void Start()
    {
         audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if (PlayerPrefs.GetInt("LoadCaseId") == 99)
        {
            SetTutorial();
        }
        else
        {
            Read();
        }
        phraseTextBox.text = eachPhrase[phraseId];
        objController = GameObject.Find("ObjetivosBG").GetComponent<ObjectivesController>();
        updateDetails();
        
        //Para perguntar o presert nos inicios de gada jogo
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            ReadStringInput();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {

            inputField.Select();
        }
    }

    public void ReadStringInput()
    {
        input = inputField.text;
        inputField.text = "";
        inputField.Select();
        ValidateWords();
        
    }

    public void NextCase(){
        phraseId++;
        if (phraseId <= eachPhrase.Length - ultimoCaso) {
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

        
    }

    void Read()
    {

        data_string = _csvFilePhrases.text;
        eachLine = new List<string>();
        eachLine.AddRange(data_string.Split("\n"[0]));
        eachPhrase = eachLine[PlayerPrefs.GetInt("LoadCaseId", 0)].Split(';');

    }

    public void ValidateWords()
    {
        if (wordsRead[phraseId] == input) //verifica se a palavra est  certa
        {
            int pos = phraseId;
            if (pos == eachPhrase.Length - 1 && ultimoCaso == 1)
            {
                audioManager.RightAnswer();
                aviso.SetActive(false);
                objController.Finish();
            }
            else if (checkPositions[pos] == false && pos != eachPhrase.Length - 1)
            {
                //wordsRead[pos] = "e5ef1a3s2de87rf0SCwfBTHYwefedse578899";

                string[] updateString = eachPhrase[pos].Split('_');
                eachPhrase[pos] = updateString[0] + input + updateString[1];
                phraseTextBox.text = eachPhrase[phraseId];
                checkPositions[pos] = true;
                audioManager.RightAnswer();
                int i = 0;
                while (checkPositions[i] == true && i < wordsRead.Count - 2)
                {
                    i++;
                }
                phraseId = i;
                phraseTextBox.text = eachPhrase[phraseId];
                objController.CountObjectivePhrase();
                updateDetails();
            }



        }
        else
        {
            CheckErrors();
            audioManager.WrongAnswer();
        }
    }

    private void updateDetails()
    {
        detalhesCaso.text = "";
        int i= 0;
        foreach (string phrase in eachPhrase)
        {
            if (i< eachPhrase.Length -1) {
                detalhesCaso.text = detalhesCaso.text + "\n\n" + phrase;
            }
            i++;
        }
    }

    public void powerUpW()
    {
        int i = 0;
        while (checkPositions[i] == true)
        {
            i++;
        }
        input = wordsRead[i];
        phraseId = i;
        phraseTextBox.text = eachPhrase[phraseId];
        ValidateWords();

    }

    public void powerUpL()
    {
        string[] updateString = eachPhrase[eachPhrase.Length - 1].Split('_');
        eachPhrase[eachPhrase.Length - 1] = updateString[0] + wordsRead[eachPhrase.Length - 1][contpw] + "_" + updateString[1];
        phraseTextBox.text = eachPhrase[phraseId];
        contpw++;

    }


    void CheckErrors()
    {
        if ((PlayerPrefs.GetInt("PrecoAjuda") == 0))
        {
            if (phraseId == eachPhrase.Length - 1)
            {
                countErrorsLast++;

            }
            else
            {
                countErrors++;
            }

            if (countErrors == 10 || countErrorsLast == 10 )
            {
                avisoFree.SetActive(true);
                canvas.SetActive(false);
            }
        }
    }

    void SetTutorial()
    {
        canvas.SetActive(false);
        avisoTutorial.SetActive(true);
        string s = "1 - A mulher achava que era a única sobrevivente de um acidente _.; 2 - Ela vagou feito alma penada por um ano por toda a _; 3 - No auge na sua depressão, ela decidiu se _; 4 - Ela então sobe no topo de um _; 5 - Ao se jogar, ela descobre não ser a única sobrevivente, pois ouve um _; Sem dúvida havia outro _";
        eachPhrase = s.Split(';');

    }
    public void LastWord()
    {
        ultimoCaso = 1;
        phraseId = eachPhrase.Length-1;
        phraseTextBox.text = eachPhrase[eachPhrase.Length - 1];
        grid.SetActive(false);
        powerUpCanvas.SetActive(false);
        powerUpLButton.SetActive(true);

        aviso.SetActive(true);

    }
}
