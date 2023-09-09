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
        seconds.text = sec.ToString();

    }
}
