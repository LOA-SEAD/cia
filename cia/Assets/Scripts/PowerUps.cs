using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUps : MonoBehaviour
{
    private int coins=0;
    [SerializeField] private TMP_Text coinDisplay;
    private Timer timer;
    private WordHunt wh;
    private InputFieldController inpFController;



    void Start()
    {
        wh = GameObject.Find("WordHunt").GetComponent<WordHunt>();
        inpFController = GameObject.Find("TelaJogo").GetComponent<InputFieldController>();
        timer = timer = GameObject.Find("TelaJogo").GetComponent<Timer>();
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
       
    }
}
