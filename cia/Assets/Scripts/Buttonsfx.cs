using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttonsfx : MonoBehaviour
{
    
    private AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        
    }

    // Update is called once per frame
    public void ButtonClick()
    {
        audioManager.ButtonClick();
    }

    public void ButtonMouseOver()
    {
        audioManager.ButtonMouseOver();
    }
}
