using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;

    public AudioSource backgroundAudioSource;
    public AudioMixer masterMixer;
    public Slider soundSlider;
    public Slider fxSlider;
    public List<AudioClip> menuSounds;
    public List<AudioClip> gameSounds;
    public float fadeSpeed = 1f;
    public bool isMute = false;

    private AudioSource track;
    private bool isMenuMusicOn = true;


    void Awake()
    {
        soundManager = this;
        track = GetComponent<AudioSource>();
        SwitchSound(true);
        Invoke("SetOptionSliders", 2);

    }

    public void SwitchSound(bool menu)
    {
        if(!isMute)
        {
            isMenuMusicOn = menu;
            StartCoroutine(FadeOut());
        }
        
    }

    IEnumerator FadeOut()
    {

        track.volume -= Time.deltaTime * fadeSpeed;

        yield return new WaitForSeconds(0);

        if(track.volume <= 0.10f)
        {
            StartCoroutine(FadeIn());
            if (isMenuMusicOn)
            {
                track.clip = menuSounds[Random.Range(0, menuSounds.Count - 1)];
            }
            else
            {
                track.clip = gameSounds[Random.Range(0, gameSounds.Count - 1)];
            }
            track.Play();
        }
        else
        {
            StartCoroutine(FadeOut());
        }

    }

    public void FadeMusicIn()
    {
        StartCoroutine(FadeIn());

    }

    IEnumerator FadeIn()
    {
        track.volume += Time.deltaTime * fadeSpeed;

        yield return new WaitForSeconds(0);

        if (track.volume <= 0.95f)
        {
            StartCoroutine(FadeIn());
        }

    }

    public void SetMusicVolume(float level)
    {
        masterMixer.SetFloat("BackgroundSound", level);
        Player.soundVolume = level;
        //Debug.Log(Player.soundVolume);
    }

    public void SetFXVolume(float level)
    {
        masterMixer.SetFloat("FX_Sound", level);
        Player.fxVolume = level;
        //Debug.Log(Player.fxVolume);
    }

    public void SetOptionSliders()
    {
        //Debug.Log(Player.soundVolume + " + " + Player.fxVolume);
        soundSlider.value = Player.soundVolume;
        fxSlider.value = Player.fxVolume;
    }

}
