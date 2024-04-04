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
    [SerializeField] private TextAsset _csvFile3;
    public List<string> caseDetails;
    public List<string> caseSize;
    public string data_string;
    public int caseID = 0;
    //public string  caseSize;
    public TMP_Text caseText;
    public TMP_Text caseTitle;
    public TMP_Text recordCaseText;
    //private float[] medalTimes = new float[] {240,210,180,210,180,150,180,150,120};
    private float[] mTimes = new float[] { 60, 90, 120, 90, 120, 150, 120, 150, 180 };
    private float[] mTimesExtra = new float[] { 40, 60, 90, 60, 90, 120, 90, 120, 150 };

    [SerializeField] TMP_Text caseSizeText;
    [SerializeField] TMP_Text goldText;
    [SerializeField] TMP_Text silverText;
    [SerializeField] TMP_Text bronzeText;
    [SerializeField] Image medalImage;
    [SerializeField] GameObject casoAberto;
    [SerializeField] GameObject casoFechado;
    [SerializeField] GameObject filtro;
    private PresetsController presets;
    [SerializeField] GameObject recordBox;
    private int countMainCases = 0;
    private int mainCasesNumber;
    public GameObject recordShow;
    public GameObject medalInfo;
    public GameObject avisoCasoExtra;
    public GameObject playButton;


    // Start is called before the first frame update
    private void Awake()
    {
        presets = GameObject.Find("PresetsController").GetComponent<PresetsController>();
        Read();
        
    }
    void Start()
    {
        
        ShowCase();
        CheckNarrative();

    }


    public void ShowCase()
    {
        presets.LoadPreferences();

        if (caseID == 0)
        {
            backButton.SetActive(false);
            nextButton.SetActive(true);
        }
        else if (caseID == caseDetails.Count - 1)
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
       
        switch (caseSize[caseID])
        {
            case "P":
                caseSizeText.text = "Tamanho do tabuleiro: Pequeno";
                break;
            case "M":
                caseSizeText.text = "Tamanho do tabuleiro: Médio";
                break;
            case "G":
                caseSizeText.text = "Tamanho do tabuleiro: Grande";
                break;

        }

        //caseSizeText.text = "Tamanho do tabuleiro: " + caseSize[caseID];
        //caseTitle.text = "Caso " + (caseID + 1);
        if (caseID >= mainCasesNumber)
        {
            filtro.SetActive(true);
            caseTitle.text = "Caso extra " + (caseID + 1 - mainCasesNumber);
            if(countMainCases < mainCasesNumber)
            {
                avisoCasoExtra.SetActive(true);
                playButton.SetActive(false);
            }
            else
            {
                avisoCasoExtra.SetActive(false);
                playButton.SetActive(true);
            }

        }
        else
        {
            avisoCasoExtra.SetActive(false);
            playButton.SetActive(true);
            filtro.SetActive(false);
            caseTitle.text = "Caso principal " + (caseID + 1);
        }

        Debug.Log("PPPPPPPPPPPPPPPPPPPPP " + countMainCases);
        SetTimeObjectives(caseID);
        ShowRecords(caseID);

    }

    public void ShowRecords(int id)
    {

        string caseIDString = "RecordeCaso" + id + presets.presetTempo.ToString() + presets.presetPreco.ToString() + presets.presetInvertida.ToString() + presets.presetDiagonal.ToString();
        float recordecaso = PlayerPrefs.GetFloat(caseIDString, 3000);
        //Debug.Log(recordecaso);
        if (presets.presetTempo != 0)
        {
            recordShow.SetActive(true);
            medalInfo.SetActive(true);
            if (recordecaso == 3000)
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
            if(id < mainCasesNumber){
                switch (caseSize[id])
                {
                    case "P":
                        if (recordecaso <= mTimes[0])
                        {
                            medalImage.sprite = GameObject.Find("Medalha Ouro").GetComponent<Image>().sprite;

                        }
                        else if (recordecaso <= mTimes[1])
                        {
                            medalImage.sprite = GameObject.Find("Medalha Prata").GetComponent<Image>().sprite;

                        }
                        else if (recordecaso <= mTimes[2])
                        {
                            medalImage.sprite = GameObject.Find("Medalha Bronze").GetComponent<Image>().sprite;

                        }
                        else
                        {
                            medalImage.sprite = GameObject.Find("Transparente").GetComponent<Image>().sprite;
                        }


                        break;
                    case "M":
                        if (recordecaso <= mTimes[3])
                        {
                            medalImage.sprite = GameObject.Find("Medalha Ouro").GetComponent<Image>().sprite;

                        }
                        else if (recordecaso <= mTimes[4])
                        {
                            medalImage.sprite = GameObject.Find("Medalha Prata").GetComponent<Image>().sprite;

                        }
                        else if (recordecaso <= mTimes[5])
                        {
                            medalImage.sprite = GameObject.Find("Medalha Bronze").GetComponent<Image>().sprite;

                        }
                        else
                        {
                            medalImage.sprite = GameObject.Find("Transparente").GetComponent<Image>().sprite;
                        }

                        break;
                    case "G":
                        if (recordecaso <= mTimes[6])
                        {
                            medalImage.sprite = GameObject.Find("Medalha Ouro").GetComponent<Image>().sprite;

                        }
                        else if (recordecaso <= mTimes[7])
                        {
                            medalImage.sprite = GameObject.Find("Medalha Prata").GetComponent<Image>().sprite;

                        }
                        else if (recordecaso <= mTimes[8])
                        {
                            medalImage.sprite = GameObject.Find("Medalha Bronze").GetComponent<Image>().sprite;

                        }
                        else
                        {
                            medalImage.sprite = GameObject.Find("Transparente").GetComponent<Image>().sprite;
                        }

                        break;
                }

            }
            else
            {
                switch (caseSize[id])
                {
                    case "P":
                        if (recordecaso <= mTimesExtra[0])
                        {
                            medalImage.sprite = GameObject.Find("Medalha Ouro").GetComponent<Image>().sprite;

                        }
                        else if (recordecaso <= mTimesExtra[1])
                        {
                            medalImage.sprite = GameObject.Find("Medalha Prata").GetComponent<Image>().sprite;

                        }
                        else if (recordecaso <= mTimesExtra[2])
                        {
                            medalImage.sprite = GameObject.Find("Medalha Bronze").GetComponent<Image>().sprite;

                        }
                        else
                        {
                            medalImage.sprite = GameObject.Find("Transparente").GetComponent<Image>().sprite;
                        }


                        break;
                    case "M":
                        if (recordecaso <= mTimesExtra[3])
                        {
                            medalImage.sprite = GameObject.Find("Medalha Ouro").GetComponent<Image>().sprite;

                        }
                        else if (recordecaso <= mTimesExtra[4])
                        {
                            medalImage.sprite = GameObject.Find("Medalha Prata").GetComponent<Image>().sprite;

                        }
                        else if (recordecaso <= mTimesExtra[5])
                        {
                            medalImage.sprite = GameObject.Find("Medalha Bronze").GetComponent<Image>().sprite;

                        }
                        else
                        {
                            medalImage.sprite = GameObject.Find("Transparente").GetComponent<Image>().sprite;
                        }

                        break;
                    case "G":
                        if (recordecaso <= mTimesExtra[6])
                        {
                            medalImage.sprite = GameObject.Find("Medalha Ouro").GetComponent<Image>().sprite;

                        }
                        else if (recordecaso <= mTimesExtra[7])
                        {
                            medalImage.sprite = GameObject.Find("Medalha Prata").GetComponent<Image>().sprite;

                        }
                        else if (recordecaso <= mTimesExtra[8])
                        {
                            medalImage.sprite = GameObject.Find("Medalha Bronze").GetComponent<Image>().sprite;

                        }
                        else
                        {
                            medalImage.sprite = GameObject.Find("Transparente").GetComponent<Image>().sprite;
                        }

                        break;
                }
            }
        }
        else
        {
            recordShow.SetActive(false);
            medalInfo.SetActive(false);
            if (recordecaso == 3000)
            {
                
                recordCaseText.text = "Não concluído";
                casoFechado.SetActive(false);
                casoAberto.SetActive(true);
            }
            else
            {

                recordCaseText.text = "Concluído com sucesso";
                casoFechado.SetActive(true);
                casoAberto.SetActive(false);
            }
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

        if (presets.presetTempo != 0)
        {
            recordBox.SetActive(true);
            string gold = "";
            string silver = "";
            string bronze = "";

            if (id < mainCasesNumber)
            {

                switch (caseSize[id])
                {
                    case "P":
                        gold = SecondsToMinutes(mTimes[0]);
                        silver = SecondsToMinutes(mTimes[1]);
                        bronze = SecondsToMinutes(mTimes[2]);
                        break;
                    case "M":
                        gold = SecondsToMinutes(mTimes[3]);
                        silver = SecondsToMinutes(mTimes[4]);
                        bronze = SecondsToMinutes(mTimes[5]);
                        break;
                    case "G":
                        gold = SecondsToMinutes(mTimes[6]);
                        silver = SecondsToMinutes(mTimes[7]);
                        bronze = SecondsToMinutes(mTimes[8]);
                        break;

                }
            }
            else
            {
                switch (caseSize[id])
                {
                    case "P":
                        gold = SecondsToMinutes(mTimesExtra[0]);
                        silver = SecondsToMinutes(mTimesExtra[1]);
                        bronze = SecondsToMinutes(mTimesExtra[2]);
                        break;
                    case "M":
                        gold = SecondsToMinutes(mTimesExtra[3]);
                        silver = SecondsToMinutes(mTimesExtra[4]);
                        bronze = SecondsToMinutes(mTimesExtra[5]);
                        break;
                    case "G":
                        gold = SecondsToMinutes(mTimesExtra[6]);
                        silver = SecondsToMinutes(mTimesExtra[7]);
                        bronze = SecondsToMinutes(mTimesExtra[8]);
                        break;

                }
            }

            goldText.text = gold;
            silverText.text = silver;
            bronzeText.text = bronze;
        }
        else
        {
            recordBox.SetActive(false);
        }

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

    public void CheckNarrative()
    {
        countMainCases = 0;
        presets.LoadPreferences();
        for (int i = 0; i < mainCasesNumber; i++)
        {
            string caseIDString = "RecordeCaso" + i + presets.presetTempo.ToString() + presets.presetPreco.ToString() + presets.presetInvertida.ToString() + presets.presetDiagonal.ToString();
            float recordecaso = PlayerPrefs.GetFloat(caseIDString, 3000);
            if (recordecaso != 3000)
            {
                countMainCases++;
                
            }
          
        }
        Debug.Log("narrativa id " + PlayerPrefs.GetInt("NarrativaId", 0));
        if (countMainCases >= (mainCasesNumber) / 2 && PlayerPrefs.GetInt("NarrativaId", 0) == 0) //checa quando entram o segundo e terceiro dialogo; Checagem muda se aumentar número de diálogos
        {
            PlayerPrefs.SetInt("NarrativaId", 1);
            SceneManager.LoadScene("Narrativa");
        }
        else if (countMainCases >= (mainCasesNumber) && PlayerPrefs.GetInt("NarrativaId", 0) == 1)
        {
            PlayerPrefs.SetInt("NarrativaId", 2);
            SceneManager.LoadScene("Narrativa");
        }
        Debug.Log("QQQQQQQQQQQQQQQQQQQQQQ " + countMainCases);
    }

    void Read()
    {

        data_string = _csvFile.text;
        caseDetails = new List<string>();
        caseDetails.AddRange(data_string.Split("\n"[0]));

        data_string = _csvFile2.text;
        caseSize = new List<string>();
        caseSize.AddRange(data_string.Split(";"[0]));

        mainCasesNumber = int.Parse(_csvFile3.text);



    }
}