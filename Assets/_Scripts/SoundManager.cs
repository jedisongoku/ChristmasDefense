using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;

    public AudioSource backgroundAudioSource;
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
    }

    public void SwitchSound(bool menu)
    {
        isMenuMusicOn = menu;
        StartCoroutine(FadeOut()); 
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

    IEnumerator FadeIn()
    {
        track.volume += Time.deltaTime * fadeSpeed;

        yield return new WaitForSeconds(0);

        if (track.volume <= 0.95f)
        {
            StartCoroutine(FadeIn());
        }

    }

}
