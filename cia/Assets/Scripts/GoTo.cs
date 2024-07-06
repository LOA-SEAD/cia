using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoTo : MonoBehaviour
{
  
    
    public void LoadCena(string cena){
        if (PlayerPrefs.GetInt("LoadCaseId") == 99 && cena== "TelaCasos" && PlayerPrefs.GetInt("PrimeiroTutorial") == 1)
        {
            cena = "MenuPrincipal";
            PlayerPrefs.SetInt("LoadCaseId", 100); //evitar loop da narrativa 
        }
        else if (PlayerPrefs.GetInt("LoadCaseId") == 99 && cena == "TelaCasos" && PlayerPrefs.GetInt("PrimeiroTutorial") == 3)
        {
            cena = "TelaCasos";
            PlayerPrefs.SetInt("LoadCaseId", 100);
        }

        int index = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("Index", index);
        Time.timeScale = 1;

        SceneManager.LoadScene(cena);
    }
}
