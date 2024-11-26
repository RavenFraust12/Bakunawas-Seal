using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Source and Clips")]
    public AudioSource bgm;
    public AudioSource sfx;
    public AudioClip[] bgmClip; //0 = Main Menu, 1 = Game, 2 = Lose
    public AudioClip[] sfxClip; //0 = button click, 1 = Select Character

    [Header("Music and Sound volume")]
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        // Load saved volume settings when the game starts
        float savedBGMVolume = PlayerPrefs.GetFloat("BGM", 1f); // Default to 1 (max volume) if no value is saved
        float savedSFXVolume = PlayerPrefs.GetFloat("SFX", 1f); // Default to 1 (max volume) if no value is saved

        // Set the sliders to the saved values
        musicSlider.value = savedBGMVolume;
        sfxSlider.value = savedSFXVolume;

        // Set the audio volumes based on the saved values
        bgm.volume = savedBGMVolume;
        sfx.volume = savedSFXVolume;
    }

    private void Update()
    {
        // Update the volume of the audio sources based on the slider values
        bgm.volume = musicSlider.value;
        sfx.volume = sfxSlider.value;

        // Save the updated volume settings
        PlayerPrefs.SetFloat("BGM", musicSlider.value);
        PlayerPrefs.SetFloat("SFX", sfxSlider.value);

        // Persist the changes in PlayerPrefs
        PlayerPrefs.Save();
    }

    public void ButtonClicks()
    {
        sfx.clip = sfxClip[0];
        sfx.Play();
    }

    public void SelectCharacter()
    {
        sfx.clip = sfxClip[1];
        sfx.Play();
    }

    public void LoseSfx()
    {
        bgm.clip = bgmClip[2];
        bgm.loop = false;
        bgm.Play();
    }
}
