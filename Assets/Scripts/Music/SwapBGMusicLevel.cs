using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapBGMusicLevel : MonoBehaviour
{
    [SerializeField] private AudioSource levelMusic;
    public static SwapBGMusicLevel instance;

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
