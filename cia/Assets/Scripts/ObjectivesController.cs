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
    void Start()
    {
        timer = GameObject.Find("TelaJogo").GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountObjective(string foundword)
    {
        contadorPalavras++;
        palavrasTexto.text = "Encontre as palavras ("+ contadorPalavras +"/" + totalPalavras + ")";
        if(contadorPalavras == totalPalavras)
        {
            CheckFinish();
        }
    }

    public void SetNumberOfWords(int number)
    {
        totalPalavras = number;
        palavrasTexto.text = "Encontre as palavras (0/" + totalPalavras + ")";
    }

    private void CheckFinish()
    {
        if(contadorPalavras == totalPalavras)
        {
            string caseIDString = "RecordeCaso" + PlayerPrefs.GetInt("LoadCaseId", 0);
            if (timer.timer > PlayerPrefs.GetFloat(caseIDString, 0)){
                PlayerPrefs.SetFloat(caseIDString, timer.timer);
            }

            SceneManager.LoadScene("TelaCasos");
        }
    }
}
