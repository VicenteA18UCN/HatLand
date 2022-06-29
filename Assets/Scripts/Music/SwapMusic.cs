using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SwapMusic : MonoBehaviour
{
    public bool isPause = false;
    public AudioMixer audioMixer;

    void Update()
    {
        audioMixer.SetFloat("volume",Mathf.Log10(PlayerPrefs.GetFloat("volume")) * 20);
        StartBackgroundMusicMenu();
        StopBackgroundMusicMenu();
        
    }

    void StopBackgroundMusicMenu()
    {
        if (SceneManager.GetActiveScene().name == "Cinematic")
        {
            BGmusic.instance.GetComponent<AudioSource>().Stop();
            this.isPause = true;
        }

        if (SceneManager.GetActiveScene().name == "Store")
        {
            BGmusic.instance.GetComponent<AudioSource>().Stop();
            this.isPause = true;
        }
    }

    void StartBackgroundMusicMenu()
    {
        if(SceneManager.GetActiveScene().name == "Menu" && this.isPause)
        {
            BGmusic.instance.GetComponent<AudioSource>().Play();
            this.isPause = false;
        }
    }
}
