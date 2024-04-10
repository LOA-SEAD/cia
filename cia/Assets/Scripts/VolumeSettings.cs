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
    [SerializeField] private Slider voiceSlider;
    private AudioManager audiomanager;
    AudioSource voiceAudioSource;

    private void Start()
    {
      audiomanager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
      voiceAudioSource = GameObject.FindGameObjectWithTag("VASource").GetComponent<AudioSource>();

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

    public void SetVoiceVolume()
    {
        float voicevolume = voiceSlider.value;
        Mixer.SetFloat("voice", Mathf.Log10(voicevolume) * 20);
        PlayerPrefs.SetFloat("voiceVolume", voicevolume);

    }

    public void SoundFeedback( AudioClip clip)
    {
        audiomanager.PlaySFX(clip);
        
    }

    public void VoiceFeedback(AudioClip clip)
    {
        if (voiceAudioSource.isPlaying)
        {
            voiceAudioSource.Stop();
        }
        audiomanager.PlayVoice(clip);

    }


    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume",0.5f);
        SetMusicVolume();
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0.5f);
        SetSfxVolume();
        voiceSlider.value = PlayerPrefs.GetFloat("voiceVolume", 0.5f);
        SetVoiceVolume();
    }
}
