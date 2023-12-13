using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ObjectivesController : MonoBehaviour
{
    [SerializeField] private TMP_Text palavrasTexto;
    [SerializeField] private TMP_Text frasesTexto;
    [SerializeField] private TMP_Text casoTexto;
    private int contadorPalavras=0;
    private int contadorFrases=0;
    private int totalPalavras = 0;
    [SerializeField]private Timer timer;
    private InputFieldController inputController;
    [SerializeField] GameObject casoEncerrado;
    private PowerUps powerUps;


    void Start()
    {
        timer = GameObject.Find("TelaJogo").GetComponent<Timer>();
        inputController = GameObject.Find("TelaJogo").GetComponent<InputFieldController>();
        powerUps = GameObject.Find("PowerUp controller").GetComponent<PowerUps>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountObjective(string foundword)
    {
        contadorPalavras++;
        palavrasTexto.text = "Encontre as palavras ("+ contadorPalavras +"/" + totalPalavras + ")";
        powerUps.IncreaseCoins();
        if(contadorPalavras == totalPalavras)
        {
            CheckFinish();
        }
    }

    public void CountObjectivePhrase()
    {
        contadorFrases++;
        frasesTexto.text = "Complete as frases (" + contadorFrases+ "/" + totalPalavras + ")";
        powerUps.IncreaseCoins();
        if (contadorFrases == totalPalavras)
        {
            CheckFinish();
        }
    }

    public void SetNumberOfWords(int number)
    {
        totalPalavras = number - 1;
        palavrasTexto.text = "Encontre as palavras (0/" + totalPalavras + ")";
    }

    private void CheckFinish()
    {
        if(contadorPalavras == totalPalavras && contadorFrases == totalPalavras)
        {
            inputController.LastWord();
        }
    }

    public void Finish()
    {
        casoTexto.text = "Conclus�o do caso (1/1)";
        casoEncerrado.SetActive(true);

        string caseIDString = "RecordeCaso" + PlayerPrefs.GetInt("LoadCaseId", 0) + PlayerPrefs.GetInt("Tempo", 0) +  PlayerPrefs.GetInt("Pre�oAjuda", 0) + PlayerPrefs.GetInt("PalavrasInvertidas", 0) +
        PlayerPrefs.GetInt("PalavrasDiagonais", 0);
        if (timer.runTimer < PlayerPrefs.GetFloat(caseIDString, 3000))
        {
            PlayerPrefs.SetFloat(caseIDString, timer.runTimer);
        }

        StartCoroutine(StartDelay());

        
    }

    public IEnumerator StartDelay()
    {
        Debug.Log("espera");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("TelaCasos");
    }
}
