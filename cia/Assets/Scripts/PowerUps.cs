using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUps : MonoBehaviour
{
    private int coins=0;
    [SerializeField] private TMP_Text coinDisplay;
    [SerializeField] private TextAsset _csvFile;
    private Timer timer;
    private WordHunt wh;
    private InputFieldController inpFController;
    private List<string> eachLine;
    public string data_string;



    void Start()
    {
        wh = GameObject.Find("WordHunt").GetComponent<WordHunt>();
        inpFController = GameObject.Find("TelaJogo").GetComponent<InputFieldController>();
        timer = timer = GameObject.Find("TelaJogo").GetComponent<Timer>();
        Read();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseCoins(){
        coins = coins + 50;
        coinDisplay.text = coins.ToString();
    }

    public void PowerUpTime()
    {
        if (coins >= 50) {
            coins = coins - 50;
            timer.timer += 60;
            coinDisplay.text = coins.ToString();

        }
    }
    public void PowerUpLetter()
    {
        if (coins >= 100)
        {
            coins = coins - 100;
            wh.DicaLetra();
            coinDisplay.text = coins.ToString();

        }
    }

    public void PowerUpWord()
    {
        if (coins >= 150)
        {
            coins = coins - 150;
            coinDisplay.text = coins.ToString();
            inpFController.powerUpW();
        }
    }

    public void FreeHint()
    {
        Application.OpenURL(eachLine[PlayerPrefs.GetInt("LoadCaseId", 0)]);
    }

    void Read()
    {

        data_string = _csvFile.text;
        eachLine = new List<string>();
        eachLine.AddRange(data_string.Split("|"[0]));
        

    }
}
