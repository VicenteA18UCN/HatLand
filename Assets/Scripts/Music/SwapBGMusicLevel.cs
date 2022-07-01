using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapBGMusicLevel : MonoBehaviour
{
    [SerializeField] private AudioSource levelMusic;
    public static SwapBGMusicLevel instance;
    [SerializeField] private AudioClip[] audioClips;

    void Awake()
    {
        KeepBackgroundMusicLevel();
    }
    // Update is called once per frame
    void Update()
    {
        StopBackgroundMusicLevel();
    }

    void StopBackgroundMusicLevel()
    {
        if (SceneManager.GetActiveScene().name == "DeathMenu" || SceneManager.GetActiveScene().name == "Menu")
        {
            Destroy(gameObject);
            levelMusic.Stop();
        }

        if(SceneManager.GetActiveScene().name == "Nivel3" && levelMusic.clip.name == "Level12")
        {
            this.levelMusic.clip = this.audioClips[0];
            this.levelMusic.Play();
            this.levelMusic.volume = 0.42f;
        }
        if(SceneManager.GetActiveScene().name == "NivelFinal" && levelMusic.clip.name == "Mine Song")
        {
            this.levelMusic.clip = this.audioClips[1];
            this.levelMusic.Play();
            this.levelMusic.volume = 0.75f;
        }
        if(SceneManager.GetActiveScene().name == "FinalCinematic" && levelMusic.clip.name == "Boss")
        {
            Destroy(gameObject);
            levelMusic.Stop();
        }
    }

    void KeepBackgroundMusicLevel()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlayBackgroundMusic(){levelMusic.Play();}
}
