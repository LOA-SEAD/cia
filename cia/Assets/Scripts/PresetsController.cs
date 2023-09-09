using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;


public class PresetsController : MonoBehaviour
{
    // Start is called before the first frame update
    private int presetTempo = 0;
    private int presetPreco = 0;
    private int presetInvertida = 0;
    private int presetDiagonal = 0;
    private int[] salvarpadrao;
    [SerializeField] TMP_Text testePreset;
    [SerializeField] private GameObject _canvas;

    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        //Debug.Log("me ajuda");

        LoadPreferences();
        //salvarpadrao[0] = presetTempo;
        //salvarpadrao[1] = presetPreco;
        //salvarpadrao[2] = presetInvertida;
        //salvarpadrao[3] = presetDiagonal;
        


    }

    void Start()
    {
        testePreset.text = presetTempo.ToString() + " " + presetPreco.ToString() + " " + presetInvertida.ToString() + " " + presetDiagonal.ToString();


    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public void DropdownOption(int index) {
        string prefName = EventSystem.current.currentSelectedGameObject.name;
        if(prefName == "Item 0: Tempo normal" || prefName == "Item 1: Tempo estendido" || prefName == "Item 2: Tempo reduzido" || prefName == "Item 3: Desativado")
        {
            PlayerPrefs.SetInt("Tempo", index);
        }
        else if (prefName == "Item 0: Sem inversão" || prefName == "Item 1: Com inversão")
        {
            PlayerPrefs.SetInt("PalavrasInvertidas", index);
        }
        else if (prefName == "Item 0: Mais barato" || prefName == "Item 1: Mais caro")
        {
            PlayerPrefs.SetInt("PreçoAjuda", index);
        }
        else if (prefName == "Item 0: Sem palavras diagonais" || prefName == "Item 1: Com palavras diagonais") { 
            PlayerPrefs.SetInt("PalavrasDiagonais", index);
        }
        
        PlayerPrefs.Save();
        
       // PlayerPrefs.SetInt(prefName, index);
         //       PlayerPrefs.Save();
        
        //      Debug.Log(PlayerPrefs.GetInt(prefName, 0));
    }

    public void PresetButton()
    {

        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "Preset1":
                PlayerPrefs.SetInt("Tempo", 0);
                PlayerPrefs.SetInt("PreçoAjuda", 0);
                PlayerPrefs.SetInt("PalavrasInvertidas", 0);
                PlayerPrefs.SetInt("PalavrasDiagonais", 0);
                break;

            case "Preset2":
                PlayerPrefs.SetInt("Tempo", 1);
                PlayerPrefs.SetInt("PreçoAjuda", 1);
                PlayerPrefs.SetInt("PalavrasInvertidas", 0);
                PlayerPrefs.SetInt("PalavrasDiagonais", 0);
               

                break;

            case "Preset3":
                PlayerPrefs.SetInt("Tempo", 1);
                PlayerPrefs.SetInt("PreçoAjuda", 1);
                PlayerPrefs.SetInt("PalavrasInvertidas", 1);
                PlayerPrefs.SetInt("PalavrasDiagonais", 1);
                break;

        }
        PlayerPrefs.Save();
        this.gameObject.SetActive(false);
        _canvas.SetActive(true);
    }
    public void BackButton()
    {
        //PlayerPrefs.SetInt("Tempo", salvarpadrao[0]);
        //PlayerPrefs.SetInt("PreçoAjuda", salvarpadrao[1]);
        //PlayerPrefs.SetInt("PalavrasInvertidas", salvarpadrao[2]);
        //PlayerPrefs.SetInt("PalavrasDiagonais", salvarpadrao[3]);
        this.gameObject.SetActive(false);
        _canvas.SetActive(true);
      

    }

    private void  LoadPreferences(){
        presetTempo = PlayerPrefs.GetInt("Tempo", 0);
        presetPreco = PlayerPrefs.GetInt("PreçoAjuda", 0);
        presetInvertida = PlayerPrefs.GetInt("PalavrasInvertidas", 0);
        presetDiagonal = PlayerPrefs.GetInt("PalavrasDiagonais", 0);
        
    }

}
