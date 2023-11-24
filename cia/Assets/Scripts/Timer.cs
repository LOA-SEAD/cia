using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timer;
    public TMP_Text minutes;
    public TMP_Text seconds;
    public float _maxTime;
    [SerializeField] GameObject timeOutText;
    [SerializeField] GameObject canvasPrincipal;
    // Start is called before the first frame update
    void Start()
    {
        timer = _maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;
        float min = Mathf.FloorToInt(timer / 60);
        float sec = Mathf.FloorToInt(timer % 60);
        minutes.text = min.ToString() + ":";
        if (sec < 10)
        {
            seconds.text ="0" + sec.ToString();
        }
        else
        {
            seconds.text = sec.ToString();
        }

        if(timer <= 0)
        {
            Time.timeScale = 0;
            TimesOut();
        }

    }

    void TimesOut()
    {
        canvasPrincipal.SetActive(false);
        timeOutText.SetActive(true);
    }
}
