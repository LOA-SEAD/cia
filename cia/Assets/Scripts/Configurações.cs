using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Configurações : MonoBehaviour
{
    
    public void VoltarCena(){

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
