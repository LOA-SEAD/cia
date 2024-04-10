using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteData : MonoBehaviour
{
    public GameObject popUp;
    public VolumeSettings volumeSettings;
    // Start is called before the first frame update
    void Start()
    {
        volumeSettings =  GameObject.Find("CanvasConfiguração").GetComponent<VolumeSettings>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeletSave()
    {
        PlayerPrefs.DeleteAll();
        volumeSettings.LoadVolume();
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void ClosePopUp()
    {
        popUp.SetActive(false);
    }

    public void OpenPopUp()
    {
        popUp.SetActive(true);
    }
}

