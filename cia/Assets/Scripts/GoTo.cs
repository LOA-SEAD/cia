using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;

public class GoTo : MonoBehaviour
{
    private static string FILE_NAME = "config.json";

    void Start()
    {
        Debug.Log("[CarregaDados] - Load() - Inicio");
        #if UNITY_WEBGL
            Debug.Log("[CarregaDados] - UNITY_WEBGL - Inicio");
            StartCoroutine(GetByHTTP());
            Debug.Log("[CarregaDados] - UNITY_WEBGL - Fim");
        #else
            GetByBSA();
        #endif
        Debug.Log("[CarregaDados] - Load() - Fim");    
    }

    IEnumerator GetByHTTP()
    {
        Debug.Log("[CarregaDados] - GetByHTTP() - Inicio");
        Debug.Log(Application.streamingAssetsPath);
        string URL = Path.Combine(Application.streamingAssetsPath, FILE_NAME);
        Debug.Log(URL);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(URL))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log("Received: " + webRequest.downloadHandler.text);
                    var jsonText = webRequest.downloadHandler.text;
                    Dados.config = JsonUtility.FromJson<GameConfig>(jsonText);
                    break;
            }
        }
    }

    private static void GetByBSA()
    { 
       Debug.Log("[CarregaDados] - GetByBSA() - Inicio");
       BetterStreamingAssets.Initialize();
       var jsonText = BetterStreamingAssets.ReadAllText("config.json");
       Dados.config = JsonUtility.FromJson<GameConfig>(jsonText);
       Debug.Log("[CarregaDados] - GetByBSA() - Fim");
    }

    public void LoadCena(string cena)
    {

        if (PlayerPrefs.GetInt("LoadCaseId") == 99 && cena == "TelaCasos" && PlayerPrefs.GetInt("PrimeiroTutorial") == 1)
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