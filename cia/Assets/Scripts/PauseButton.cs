using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        this.gameObject.SetActive(true);
        _canvas.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
        _canvas.SetActive(true);
    }

    public void QuitButton()
    {
        this.gameObject.SetActive(true);
        _canvas.SetActive(false);
    }

    public void NoQuitButton()
    {
        this.gameObject.SetActive(false);
        _canvas.SetActive(true);
    }
}
