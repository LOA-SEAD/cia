using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectivesController : MonoBehaviour
{
    [SerializeField] private TMP_Text palavrasTexto;
    [SerializeField] private TMP_Text frasesTexto;
    [SerializeField] private TMP_Text casoTexto;
    private int contadorpalavras=0;
    private int contadorfrases=0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountObjective(string foundword)
    {
        contadorpalavras++;
        palavrasTexto.text = "Encontre as palavras ("+ contadorpalavras +"/5)";
    }
}
