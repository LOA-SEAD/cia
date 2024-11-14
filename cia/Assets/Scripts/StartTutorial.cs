using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvas;
    public GameObject avisoTutorial;
    public string tutorialWords = "ajudante;londres;inimigo;dedução;autor;holmes";
    public string tutorialPhrases = "1 - Ele é acompanhado por um _ chamado Watson.; 2 - Um detetive famoso nas ruas de _; 3 - Seu maior _ é Moriarty; 4 - Dizem que ele é um mestre da _; 5 - O _ de seus livros é Arthur Conan Doyle; Sem dúvida, meu rival só pode ser Sherlock _";
    public string tutorialFreeHint = "Não precisa ter vergonha de usar vantagens disponíveis a seu favor. Consultar materiais extras pode ser útil para solucionar casos. Por exemplo, como saberia sem esta consulta que o sobrenome desse detetive londrino tem 6 letras?";



    public void StartTutorialCase()
    {
        PlayerPrefs.SetInt("Tempo", 0);
        PlayerPrefs.SetInt("PrecoAjuda", 0);
        PlayerPrefs.SetInt("PalavrasInvertidas", 0);
        PlayerPrefs.SetInt("PalavrasDiagonais", 0);
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
