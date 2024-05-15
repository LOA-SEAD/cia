using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

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
        //phraseTextBox.text = eachPhrase[phraseId];
        ReplaceUnderline();
        objController = GameObject.Find("ObjetivosBG").GetComponent<ObjectivesController>();
        UpdateDetails();
        
        
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
        inputField.Select();
        input = inputField.text;
        inputField.text = "";
        
        ValidateWords();
        EventSystem.current.SetSelectedGameObject(null);

    }

    public void NextCase(){
        phraseId++;
        if (phraseId <= eachPhrase.Length - ultimoCaso) {
            ReplaceUnderline();
            //phraseTextBox.text = eachPhrase[phraseId];
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
            //phraseTextBox.text = eachPhrase[phraseId];
            ReplaceUnderline();
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
        if (wordsRead[phraseId] == input.ToLower()) //verifica se a palavra est  certa
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
                //phraseTextBox.text = eachPhrase[phraseId];
                ReplaceUnderline();
                checkPositions[pos] = true;
                audioManager.RightAnswer();
                int i = 0;
                while (checkPositions[i] == true && i < wordsRead.Count - 2)
                {
                    i++;
                }
                phraseId = i;
                //phraseTextBox.text = eachPhrase[phraseId];
                ReplaceUnderline();
                objController.CountObjectivePhrase();
                UpdateDetails();
            }



        }
        else
        {
            CheckErrors();
            audioManager.WrongAnswer();
        }
    }

    private void UpdateDetails()
    {
        detalhesCaso.text = "";
        int i= 0;
        foreach (string phrase in eachPhrase)
        {
            if (i< eachPhrase.Length -1) {
                detalhesCaso.text = detalhesCaso.text + "\n\n" + phrase.Replace("_", "_____");
            }
            i++;
        }
    }

    public void PowerUpW()
    {
        int i = 0;
        while (checkPositions[i] == true)
        {
            i++;
        }
        input = wordsRead[i];
        phraseId = i;
        ReplaceUnderline();
        //phraseTextBox.text = eachPhrase[phraseId];
        ValidateWords();

    }

    public void PowerUpL()
    {
        string[] updateString = eachPhrase[eachPhrase.Length - 1].Split('_');
        eachPhrase[eachPhrase.Length - 1] = updateString[0] + wordsRead[eachPhrase.Length - 1][contpw] + "_" + updateString[1];
        //phraseTextBox.text = eachPhrase[phraseId];
        ReplaceUnderline();
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
        string s = "1 - Ele � acompanhado por um ajudante chamado _.; 2 - Um detetive famoso nas ruas de _; 3 - Seu maior inimigo �  _; 4 - Dizem que ele � um mestre da _; 5 - Seus livros foram escritos por Arthur Conan _; Sem d�vida, meu rival s� pode ser Sherlock _";
        eachPhrase = s.Split(';');

    }
    public void LastWord()
    {
        ultimoCaso = 1;
        phraseId = eachPhrase.Length-1;
        //phraseTextBox.text = eachPhrase[eachPhrase.Length - 1];
        ReplaceUnderline();
        grid.SetActive(false);
        powerUpCanvas.SetActive(false);
        powerUpLButton.SetActive(true);

        aviso.SetActive(true);

    }

    public void ReplaceUnderline()
    {
        phraseTextBox.text = eachPhrase[phraseId].Replace("_", "_____");
    }
}
