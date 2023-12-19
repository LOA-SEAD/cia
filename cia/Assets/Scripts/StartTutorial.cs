using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTutorial : MonoBehaviour
{
    // Start is called before the first frame update

    public void StartTutorialCase()
    {
        PlayerPrefs.SetInt("Tempo", 0);
        PlayerPrefs.SetInt("PreçoAjuda", 1);
        PlayerPrefs.SetInt("PalavrasInvertidas", 1);
        PlayerPrefs.SetInt("PalavrasDiagonais", 1);
        PlayerPrefs.SetInt("LoadCaseId", 99);
        PlayerPrefs.SetString("LoadCaseSize", "G");
        SceneManager.LoadScene("TelaJogo");

    }

}
