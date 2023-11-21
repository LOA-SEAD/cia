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
    public List<string> caseDetails;
    public string data_string;
    public int caseID=0;    
    public TMP_Text caseText;
    public TMP_Text caseTitle;
    public TMP_Text recordCaseText;


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
        caseTitle.text = "Caso " + (caseID+1);
        ShowRecords(caseID);

    }

    public void ShowRecords(int id)
    {
        string caseIDString = "RecordeCaso" + id;
        int recordecaso = PlayerPrefs.GetInt(caseIDString, 0);
        Debug.Log(recordecaso);
        if(recordecaso ==0)
        {
            recordCaseText.text = "Sem recordes";
        }
        else
        {
            recordCaseText.text = recordecaso.ToString();
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
        SceneManager.LoadScene("TelaJogo");
    }


    void Read()
    {
        
        data_string = _csvFile.text;
        caseDetails = new List<string>();
        caseDetails.AddRange(data_string.Split("\n"[0]));

    }
}
