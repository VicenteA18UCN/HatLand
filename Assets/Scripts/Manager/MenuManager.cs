using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MenuManager : MonoBehaviour
{
    private AudioManager audioManager;
    
    private void OnEnable(){
        this.audioManager = GetComponent<AudioManager>();
    }
    public void OnMouseOverNoButton()
    {
        return;
    }

    public void OnClickPlayGameButton()
    {
        LevelManager.LoadNextLevel();      
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
}