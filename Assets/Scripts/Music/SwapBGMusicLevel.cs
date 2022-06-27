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
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "DeathMenu" || SceneManager.GetActiveScene().name == "Menu")
        {
            Destroy(gameObject);
            levelMusic.Stop();
        }
    }

    public void PlayBackgroundMusic(){
        levelMusic.Play();print("play");
        }
}
