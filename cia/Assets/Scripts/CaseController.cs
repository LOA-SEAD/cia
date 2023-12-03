using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CaseController : MonoBehaviour
{

    public string[] data_sentences;
    public int line;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject backButton;
    [SerializeField] private TextAsset _csvFile;
    [SerializeField] private TextAsset _csvFile2;
    public List<string> caseDetails;
    public List<string> caseSize;
    public string data_string;
    public int caseID=0;
    //public string  caseSize;
    public TMP_Text caseText;
    public TMP_Text caseTitle;
    public TMP_Text recordCaseText;
    public float[] medalTimes = new float[] {240,210,180,210,180,150,180,150,120}; 
    [SerializeField] TMP_Text caseSizeText;
    [SerializeField] TMP_Text goldText;
    [SerializeField] TMP_Text silverText;
    [SerializeField] TMP_Text bronzeText;
    [SerializeField] Image medalImage;
    [SerializeField] GameObject casoAberto;
    [SerializeField] GameObject casoFechado;


    // Start is called before the first frame update
    private void Awake()
    {
        Read();

    }
    void Start()
    {
        ShowCase();
        
    }


    public void ShowCase()
    {
        //Debug.Log(data_values[1]);
        if (caseID == 0)
        {
            backButton.SetActive(false);
            nextButton.SetActive(true);
        }
        else if (caseID == caseDetails.Count-1)
        {
            nextButton.SetActive(false);
            backButton.SetActive(true);
        }
        else
        {
            backButton.SetActive(true);
            nextButton.SetActive(true);
        }
  
        caseText.text = "";
        caseText.text = caseDetails[caseID];
        caseSizeText.text = "Tamanho do tabuleiro: " +  caseSize[caseID];
        caseTitle.text = "Caso " + (caseID+1);
        SetTimeObjectives(caseID);
        ShowRecords(caseID);

    }

    public void ShowRecords(int id)
    {
        string caseIDString = "RecordeCaso" + id;
        float recordecaso = PlayerPrefs.GetFloat(caseIDString, 0);
        Debug.Log(recordecaso);
        if(recordecaso ==0)
        {
            recordCaseText.text = "Sem recordes";
            casoFechado.SetActive(false);
            casoAberto.SetActive(true);
        }
        else
        {
            recordCaseText.text = SecondsToMinutes(recordecaso);
            casoFechado.SetActive(true);
            casoAberto.SetActive(false);
        }
        switch (caseSize[id])
        {
            case "P":
                if (recordecaso >= medalTimes[0])
                {
                    medalImage.sprite = GameObject.Find("Medalha Ouro").GetComponent<Image>().sprite;

                }
                else if (recordecaso >= medalTimes[1])
                {
                    medalImage.sprite = GameObject.Find("Medalha Prata").GetComponent<Image>().sprite;

                }
                else if (recordecaso >= medalTimes[2])
                {
                    medalImage.sprite = GameObject.Find("Medalha Bronze").GetComponent<Image>().sprite;

                }
                else
                {
                    medalImage.sprite = null;
                }

                break;
            case "M":
                if (recordecaso >= medalTimes[3])
                {
                    medalImage.sprite = GameObject.Find("Medalha Ouro").GetComponent<Image>().sprite;

                }
                else if (recordecaso >= medalTimes[4])
                {
                    medalImage.sprite = GameObject.Find("Medalha Prata").GetComponent<Image>().sprite;

                }
                else if (recordecaso >= medalTimes[5])
                {
                    medalImage.sprite = GameObject.Find("Medalha Bronze").GetComponent<Image>().sprite;

                }
                else
                {
                    medalImage.sprite = null;

                }
                break;
            case "G":
                if (recordecaso >= medalTimes[6])
                {
                    medalImage.sprite = GameObject.Find("Medalha Ouro").GetComponent<Image>().sprite;

                }
                else if (recordecaso >= medalTimes[7])
                {
                    medalImage.sprite = GameObject.Find("Medalha Prata").GetComponent<Image>().sprite;

                }
                else if (recordecaso >= medalTimes[8])
                {
                    medalImage.sprite = GameObject.Find("Medalha Bronze").GetComponent<Image>().sprite;

                }
                else
                {
                    medalImage.sprite = null;

                }
                break;

        }


    }


    public void NextId()
    {
        caseID++;
        ShowCase();
        
    }
    public void BackId()
    {
        caseID--;
        ShowCase();
    }

    public void StartCase()
    {
        PlayerPrefs.SetInt("LoadCaseId", caseID);
        PlayerPrefs.SetString("LoadCaseSize", caseSize[caseID]);
        SceneManager.LoadScene("TelaJogo");
    }


    void SetTimeObjectives(int id)
    {
        string gold = "";
        string silver = "";
        string bronze = "";

        switch (caseSize[id]){
            case "P":
                gold = SecondsToMinutes(medalTimes[0]);
                silver = SecondsToMinutes(medalTimes[1]);
                bronze = SecondsToMinutes(medalTimes[2]);
                break;
            case "M":
                gold = SecondsToMinutes(medalTimes[3]);
                silver = SecondsToMinutes(medalTimes[4]);
                bronze = SecondsToMinutes(medalTimes[5]);
                break;
            case "G":
                gold = SecondsToMinutes(medalTimes[6]);
                silver = SecondsToMinutes(medalTimes[7]);
                bronze = SecondsToMinutes(medalTimes[8]);
                break;

        }

        goldText.text = gold;
        silverText.text = silver;
        bronzeText.text = bronze;
    } 

    string SecondsToMinutes(float sec)
    {
        string time = "";
        float minutes = Mathf.FloorToInt(sec / 60);
        float seconds = Mathf.FloorToInt(sec % 60);
        if (seconds < 10)
        {
            time = minutes.ToString() + ":0" + seconds.ToString();
        }
        else
        {
            time = minutes.ToString() + ":" + seconds.ToString();
        }

        return time;
    }

    void Read()
    {
        
        data_string = _csvFile.text;
        caseDetails = new List<string>();
        caseDetails.AddRange(data_string.Split("\n"[0]));

        data_string = _csvFile2.text;
        caseSize = new List<string>();
        caseSize.AddRange(data_string.Split(";"[0]));


    }
}
