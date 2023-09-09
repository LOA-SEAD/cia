using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoTo : MonoBehaviour
{
    public void LoadCena(string cena){
        int index = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("Index", index);
        Time.timeScale = 1;
        SceneManager.LoadScene(cena);
    }
}
