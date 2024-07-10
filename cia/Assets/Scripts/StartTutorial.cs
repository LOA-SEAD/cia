using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;
    public GameObject avisoTutorial;
    public string tutorialWords = "watson;londres;moriarty;dedução;doyle;holmes";
    public string tutorialPhrases = "1 - Ele é acompanhado por um ajudante chamado _.; 2 - Um detetive famoso nas ruas de _; 3 - Seu maior inimigo é  _; 4 - Dizem que ele é um mestre da _; 5 - Seus livros foram escritos por Arthur Conan _; Sem dúvida, meu rival só pode ser Sherlock _";
    public string tutorialFreeHint = "Não precisa ter vergonha de usar vantagens disponíveis a seu favor. Consultar materiais extras pode ser útil para solucionar casos. Por exemplo, como saberia sem esta consulta que o sobrenome desse detetive londrino tem 6 letras?";



    public void StartTutorialCase()
    {
        PlayerPrefs.SetInt("Tempo", 0);
        PlayerPrefs.SetInt("PrecoAjuda", 0);
        PlayerPrefs.SetInt("PalavrasInvertidas", 1);
        PlayerPrefs.SetInt("PalavrasDiagonais", 1);
        PlayerPrefs.SetInt("LoadCaseId", 99);
        PlayerPrefs.SetString("LoadCaseSize", "M");
        SceneManager.LoadScene("TelaJogo");

    }

    public string SetTutorial()
    {
        if (PlayerPrefs.GetInt("PrimeiroTutorial") == 2)
        {
            PlayerPrefs.SetInt("PrimeiroTutorial", 3);
        }
        else
        {
            PlayerPrefs.SetInt("PrimeiroTutorial", 1);
        }
        canvas.SetActive(false);
        avisoTutorial.SetActive(true);
        return tutorialPhrases;
        

    }





}
