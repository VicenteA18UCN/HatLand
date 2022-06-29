using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;


public class MenuManager : MonoBehaviour
{
    private AudioManager audioManager;
    [SerializeField] private GameObject levelMusic;
    [SerializeField] private GameObject ContinueGame;

    private void OnEnable(){
        this.audioManager = GetComponent<AudioManager>();
    }

    void Start()
    {
        if(PlayerPrefs.HasKey("Level"))
        {
            ContinueGame.SetActive(true);
        } else
        {
            ContinueGame.SetActive(false);
        }
    }
    public void OnMouseOverNoButton()
    {
        return;
    }

    public void OnClickPlayGameButton()
    {
        LevelManager.LoadFirstLevel();
        levelMusic.GetComponent<SwapBGMusicLevel>().PlayBackgroundMusic();      
    }

    public void OnClickSound()
    {
        audioManager.PlaySound("Button Sound",0.5f);  
    }

    public void OnClickStoreButton()
    {
        LevelManager.LoadStore();
    }

    public void OnClickDeathButton()
    {
        
        LevelManager.LoadMenu();

    }

    public void OnClickBackMenu()
    {
        LevelManager.BackMenu();
    }

    public void OnClickSettings()
    {
        LevelManager.SettingMenu();
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickContinue()
    {  
        SceneManager.LoadScene(PlayerPrefs.GetString("Level"));
        GameManager.Continue = true;
    }
}