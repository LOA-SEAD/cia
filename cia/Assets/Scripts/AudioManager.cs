using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource sfx;

    public AudioClip background;
    public AudioClip coffee;
    public AudioClip idea;
    public AudioClip information;
    public AudioClip rightanswer;
    public AudioClip page;
    public AudioClip wronganswer;
    public AudioClip mouseOver;
    public AudioClip mouseClick;
    [SerializeField] GameObject config;
    

    private VolumeSettings volSettings;

    private void Awake()
    {
        volSettings = config.GetComponent<VolumeSettings>();
    }
    private void Start()
    {
        PlayBGSong(background);
        
        
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            volSettings.LoadVolume();
            
        }
        else
        {
            volSettings.SetMusicVolume();
          
        }
        config.SetActive(false);

        DontDestroyOnLoad(this.gameObject);

        //Para não perguntar preset todos os rounds
        PlayerPrefs.SetInt("Tempo", 10);
    }

    public void ButtonMouseOver()
    {
        PlaySFX(mouseOver);
    }

    public void ButtonClick()
    {
        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "PowerUp1":
                PlaySFX(coffee);

                break;

            case "PowerUp2":
                PlaySFX(idea);


                break;

            case "PowerUp3":
                PlaySFX(information);
                break;

            case "CasoAnterior":
                PlaySFX(page);
                break;

            case "CasoSeguinte":
                PlaySFX(page);
                break;

            default:
                PlaySFX(mouseClick);
                break;

        }
       
    }

    public void RightAnswer()
    {
        sfx.PlayOneShot(rightanswer);
    }

    public void WrongAnswer()
    {
        sfx.PlayOneShot(wronganswer);
    }

    public void PlayBGSong(AudioClip clip)
    {
        music.clip = clip;
        music.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }



   
}