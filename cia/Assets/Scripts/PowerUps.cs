using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour
{
    private float coins=0;
    [SerializeField] private TMP_Text coinDisplay;
    [SerializeField] private TextAsset _csvFile;
    private Timer timer;
    private WordHunt wh;
    private ObjectivesController objContr;
    private InputFieldController inpFController;
    private List<string> eachLine;
    public string data_string;
    float cheaperPW = 1;
    public Button[] powerUpButton;
    [SerializeField] GameObject avisoFree;
    private bool[] ajudasUsadas = new bool[] {false, false, false};
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject canvasConsulta;
    [SerializeField] private TMP_Text consultaText;



    void Start()
    {
        wh = GameObject.Find("WordHunt").GetComponent<WordHunt>();
        inpFController = GameObject.Find("TelaJogo").GetComponent<InputFieldController>();
        objContr = GameObject.Find("ObjetivosBG").GetComponent<ObjectivesController>();
        timer = timer = GameObject.Find("TelaJogo").GetComponent<Timer>();
        Read();
        if (PlayerPrefs.GetInt("PrecoAjuda") == 0)
        {
            cheaperPW = 0.5f;
        }
        checkCoins();
        initButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseCoins(){

        coins = coins + 40;
        coinDisplay.text = coins.ToString();
        checkCoins();
    }

    public void PowerUpTime()
    {
        if (coins >= 50 * cheaperPW) {
            coins = coins - 50 * cheaperPW;
            timer.timer += 60;
            coinDisplay.text = coins.ToString();
            checkCoins();

        }
    }
    public void PowerUpLetter()
    {
        if (coins >= 100 * cheaperPW)
        {
            coins = coins - 100 * cheaperPW;
            wh.DicaLetra();
            coinDisplay.text = coins.ToString();
            checkCoins();

        }
    }

    public void PowerUpWord()
    {
        if (coins >= 150 * cheaperPW)
        {
            coins = coins - 150 * cheaperPW;
            coinDisplay.text = coins.ToString();
            inpFController.powerUpW();
            checkCoins();
        }
    }

    public void PowerUpLast()
    {
        if (coins >= 250 * cheaperPW)
        {
            coins = coins - 250 * cheaperPW;
            coinDisplay.text = coins.ToString();
            inpFController.powerUpL();
            checkCoins();
        }
    }

    public void FreeHint()
    {
        int id = PlayerPrefs.GetInt("LoadCaseId", 0);
        if (id == 99)
        {
            consultaText.text = "Não precisa ter vergonha de usar vantagens disponíveis a seu favor. Consultar materiais extras pode ser útil para solucionar casos. Por exmplo, como saberia sem esta consulta que a suposta mulher não era a única na cidade após o incidente?";
        }
        else
        {
            consultaText.text = eachLine[id];
            
        }
        canvasConsulta.SetActive(true);
        //Application.OpenURL(eachLine[PlayerPrefs.GetInt("LoadCaseId", 0)]);
    }

    public void initButtons()
    {
        TMP_Text a;
        float b;
        if (PlayerPrefs.GetInt("Tempo", 0) == 1)
        {
            a = powerUpButton[0].GetComponentInChildren<TMP_Text>();
            b = 50 * cheaperPW;
            a.text = b.ToString() + " - Mais tempo";
        }
        else
        {
            a = powerUpButton[0].GetComponentInChildren<TMP_Text>();
            a.text =  "Power up indisponível";
        }

        a = powerUpButton[1].GetComponentInChildren<TMP_Text>();
        b = 100 * cheaperPW;
        a.text = b.ToString() + " - Revela uma letra";

        a = powerUpButton[2].GetComponentInChildren<TMP_Text>();
        b = 150 * cheaperPW;
        a.text = b.ToString() + " - Completa uma pista";

        a = powerUpButton[3].GetComponentInChildren<TMP_Text>();
        b = 250 * cheaperPW;
        a.text = b.ToString() + " - Revela uma letra da palavra final";

    }

    private void checkCoins()
    {
        if(coins >= 50 * cheaperPW && cheaperPW == 1)
        {
            powerUpButton[0].interactable = true;
        }
        else
        {
            powerUpButton[0].interactable = false;
        }

        if (coins >= 100 * cheaperPW && objContr.contadorPalavras != objContr.totalPalavras)
        {
            //Debug.Log(objContr.allwordsfound);
            powerUpButton[1].interactable = true;

        }
        else
        {
            powerUpButton[1].interactable = false;
        }

        if (coins >= 150 * cheaperPW)
        {
            powerUpButton[2].interactable = true;

        }
        else
        {
            powerUpButton[2].interactable = false;
        }

        if (coins >= 250 * cheaperPW )
        {
            powerUpButton[3].interactable = true;

        }
        else
        {
            powerUpButton[3].interactable = false;
        }
    }

    public void FreePW()
    {
        if(wh.countErrors == 10 && ajudasUsadas[0]== false)
        {

            ajudasUsadas[0] = true;
            coins = 50;
            PowerUpLetter();
        }
        else if (inpFController.countErrors == 10 && ajudasUsadas[1] == false)
        {
            ajudasUsadas[1] = true;
            coins = 75;
            PowerUpWord();
        }
        else if (inpFController.countErrorsLast == 10 && ajudasUsadas[2] == false)
        {
            ajudasUsadas[2] = true;
            coins = 125;
            PowerUpLast();
        }
        canvas.SetActive(true);
        avisoFree.SetActive(false);
    }

    void Read()
    {

        data_string = _csvFile.text;
        eachLine = new List<string>();
        eachLine.AddRange(data_string.Split("|"[0]));
        

    }
}
