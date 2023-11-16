using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer Mixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioClip testSFX;
    private AudioManager audiomanager;

    private void Start()
    {
      audiomanager = GameObject.Find("AudioManager").GetComponent<AudioManager>();


    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        Mixer.SetFloat("music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSfxVolume()
    {
        float sfxvolume = sfxSlider.value;
        Mixer.SetFloat("sfx", Mathf.Log10(sfxvolume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", sfxvolume);
        
    }

    public void SoundFeedback()
    {
        audiomanager.PlaySFX(testSFX);
        
    }

    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume();
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetSfxVolume();
    }
}
