﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour
{
    public string[] data_sentences;
    public string[] data_balloons;
    public string[] data_expressions;
    public int line;
    [SerializeField] private TextAsset dialogueFile;
    public List<string> eachLine;
    public string data_string;
    private Queue<string> sentences;
    //private Queue<RectTransform> positions;
    private Queue<int> balloons;
    private Queue<int> expressions;
    public TMP_Text[] dialogueText;
    public GameObject[] expressionsSprites;
    public GameObject[] balloonsSprites;
    int currentBalloon = 0;
    int currentExpression=0;
    private AudioManager audioManager;
    public AudioClip music;
    public AudioClip[] voices;
    //public Animator fade;
    public GameObject levelChanger;
    private int curVoice = 0;
    private int[] voiceCut = {0,7,9 };
    int id;
    AudioSource voiceAudioSource;
    [SerializeField] private GameObject credits;
    StartTutorial startTut;

    //public GameObject fadeIn;

    // Start is called before the first frame update
    private void Awake()
    {
        Read();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        voiceAudioSource = GameObject.FindGameObjectWithTag("VASource").GetComponent<AudioSource>();

        //fade = GameObject.Find("LevelChanger").GetComponent<Animator>();
        audioManager.PlayBGSong(music);
        

    }
    void Start()
    {
        //fade.SetTrigger("FadeIn");
        sentences = new Queue<string>(); 
        expressions = new Queue<int>();
        balloons = new Queue<int>();
        


    }

    // Update is called once per frame
   

    public void StartDialogue(Dialogue dialogue)
    {
  
        sentences.Clear();
        int i = 0;
        foreach (string sentence in data_sentences)
        {
            sentences.Enqueue(sentence);
            balloonsSprites[currentBalloon].SetActive(false);
            balloons.Enqueue(int.Parse(data_balloons[i]));
            
            expressionsSprites[currentExpression].SetActive(false);
            expressions.Enqueue(int.Parse(data_expressions[i]));

            i++;
            
        }
        curVoice = voiceCut[id/3];
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        balloonsSprites[currentBalloon].SetActive(false);
        expressionsSprites[currentExpression].SetActive(false);
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        

        currentBalloon = balloons.Dequeue();
        currentExpression = expressions.Dequeue();
        balloonsSprites[currentBalloon].SetActive(true);
        expressionsSprites[currentExpression].SetActive(true);


        VoiceStop();
        audioManager.PlayVoice(voices[curVoice]);
        dialogueText[currentBalloon].text = sentence;
        curVoice++;
    }

    public void EndDialogue()
    {
        expressionsSprites[0].SetActive(true);
        if (PlayerPrefs.GetInt("Final", 123) == 0)
        {
            credits.SetActive(true);
        }
        else
        {
            ChangeSceneCounter();
        }
        
    }

    public void ChangeSceneCounter()
    {
        levelChanger.SetActive(true);
        VoiceStop();
        StartCoroutine(StartDelay());
    }

    public IEnumerator StartDelay()
    {
        VoiceStop();
        yield return new WaitForSeconds(1.0f);
        if (PlayerPrefs.GetInt("PrimeiroTutorial", 0) == 0)
        {
            startTut = GameObject.Find("Start Tutorial").GetComponent<StartTutorial>();

            PlayerPrefs.SetInt("PrimeiroTutorial", 2);
            startTut.StartTutorialCase();

        }
        else
        {
            SceneManager.LoadScene("TelaCasos");
        }
    }

    public void VoiceStop()
    {
        if (voiceAudioSource.isPlaying)
        {
            voiceAudioSource.Stop();
        }
    }


    void Read()
    {
        id = PlayerPrefs.GetInt("NarrativaId", 0);
        id = id * 3;

        data_string = dialogueFile.text;
        eachLine = new List<string>();
        eachLine.AddRange(data_string.Split("\n"[0]));
        data_sentences = eachLine[id].Split(';');
        data_balloons = eachLine[id + 1].Split(';');
        data_expressions = eachLine[id + 2].Split(';');


    }
}
