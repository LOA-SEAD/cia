using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;


public class PresetsController : MonoBehaviour
{
    // Start is called before the first frame update
    public int presetTempo = 0;
    public int presetPreco = 0;
    public int presetInvertida = 0;
    public int presetDiagonal = 0;
    private int[] salvarpadrao;
    
    [SerializeField] private GameObject _canvas;
    private CaseController caseController;
    [SerializeField]private GameObject canvasPersonalizar;
    [SerializeField] private GameObject canvasPreset;
    public ToggleGroup tempoGroup;
    public ToggleGroup ajudaGroup;
    public ToggleGroup invertidasGroup;
    public ToggleGroup diagonalGroup;


    void Awake()
    {


    }

    void Start()
    {
        
       caseController = GameObject.Find("CaseController").GetComponent<CaseController>();
       checkPresetChoice();

    }

    // Update is called once per frame
    void Update()
    {
       
    }



    public void RadioButtonSave()
    {
        Toggle toggle1 = tempoGroup.ActiveToggles().FirstOrDefault();
        Toggle toggle2 = ajudaGroup.ActiveToggles().FirstOrDefault();
        Toggle toggle3 = invertidasGroup.ActiveToggles().FirstOrDefault();
        Toggle toggle4 = diagonalGroup.ActiveToggles().FirstOrDefault();
        
        if (toggle1.name == "Sem Tempo")
        {
            PlayerPrefs.SetInt("Tempo", 0);
        }
        else if (toggle1.name == "Tempo padr�o")
        {
            PlayerPrefs.SetInt("Tempo", 1);
        }
        

        if (toggle2.name == "Pre�o reduzido")
        {
            PlayerPrefs.SetInt("PrecoAjuda", 0);
        }
        else if (toggle2.name == "Pre�o padr�o")
        {
            PlayerPrefs.SetInt("PrecoAjuda", 1);
        }

        if (toggle3.name == "Desabilitado")
        {
            PlayerPrefs.SetInt("PalavrasInvertidas", 0);
        }
        else if (toggle3.name == "Habilitado")
        {
            PlayerPrefs.SetInt("PalavrasInvertidas", 1);
        }

        if (toggle4.name == "Desabilitado")
        {
            PlayerPrefs.SetInt("PalavrasDiagonais", 0);
        }
        else if (toggle4.name == "Habilitado")
        {
            PlayerPrefs.SetInt("PalavrasDiagonais", 1);
        }

        SavePresetButton();
    }

    public void PresetButton()
    {

        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "Preset1":
                PlayerPrefs.SetInt("Tempo", 0);
                PlayerPrefs.SetInt("PrecoAjuda", 0);
                PlayerPrefs.SetInt("PalavrasInvertidas", 0);
                PlayerPrefs.SetInt("PalavrasDiagonais", 0);
                break;

            case "Preset2":
                PlayerPrefs.SetInt("Tempo", 1);
                PlayerPrefs.SetInt("PrecoAjuda", 1);
                PlayerPrefs.SetInt("PalavrasInvertidas", 0);
                PlayerPrefs.SetInt("PalavrasDiagonais", 0);
               

                break;

            case "Preset3":
                PlayerPrefs.SetInt("Tempo", 1);
                PlayerPrefs.SetInt("PrecoAjuda", 1);
                PlayerPrefs.SetInt("PalavrasInvertidas", 1);
                PlayerPrefs.SetInt("PalavrasDiagonais", 1);
                break;

        }
        PlayerPrefs.Save();
        LoadPreferences();
       canvasPreset.SetActive(false);
        _canvas.SetActive(true);
        caseController.CheckNarrative();       
        caseController.ShowCase();
    }
    public void BackButton()
    {

        
        this.gameObject.SetActive(false);
        _canvas.SetActive(true);
      

    }

    public void  LoadPreferences(){
        presetTempo = PlayerPrefs.GetInt("Tempo", 0);
        presetPreco = PlayerPrefs.GetInt("PrecoAjuda", 0);
        presetInvertida = PlayerPrefs.GetInt("PalavrasInvertidas", 0);
        presetDiagonal = PlayerPrefs.GetInt("PalavrasDiagonais", 0);
        
    }

 

    public void SavePresetButton()
    {
        LoadPreferences();
        caseController.ShowCase();
        caseController.CheckNarrative();
        canvasPersonalizar.SetActive(false);
        _canvas.SetActive(true);
    }

    void OpenCustomization()
    {
        canvasPreset.SetActive(false);
        canvasPersonalizar.SetActive(true);
        tempoGroup = GetComponent<ToggleGroup>();
        ajudaGroup = GetComponent<ToggleGroup>();
        invertidasGroup = GetComponent<ToggleGroup>();
        diagonalGroup = GetComponent<ToggleGroup>();
    }

    void checkPresetChoice()
    {
        if (PlayerPrefs.GetInt("Tempo", 10) ==10)
        {
            _canvas.SetActive(false);
            canvasPreset.SetActive(true);
        }
    }

}
