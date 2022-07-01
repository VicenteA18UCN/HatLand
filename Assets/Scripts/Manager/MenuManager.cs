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

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.Continue = true;
            PlayerPrefs.SetFloat("X",-5.45f);
            PlayerPrefs.SetFloat("Y",-3.12f);
            PlayerPrefs.SetFloat("Z",0);
            SceneManager.LoadScene("Nivel1");
        }
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.Continue = true;
            PlayerPrefs.SetFloat("X",8.236f);
            PlayerPrefs.SetFloat("Y",-0.189f);
            PlayerPrefs.SetFloat("Z",0);
            SceneManager.LoadScene("Nivel2");
        }
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameManager.Continue = true;
            PlayerPrefs.SetFloat("X",-5.694f);
            PlayerPrefs.SetFloat("Y",1.067f);
            PlayerPrefs.SetFloat("Z",0);
            SceneManager.LoadScene("Nivel3");
        }
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameManager.Continue = true;
            PlayerPrefs.SetFloat("X",-5.406f);
            PlayerPrefs.SetFloat("Y",-2.204f);
            PlayerPrefs.SetFloat("Z",0);
            SceneManager.LoadScene("Nivel4");
        }
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha5))
        {
            GameManager.Continue = true;
            PlayerPrefs.SetFloat("X",37.3f);
            PlayerPrefs.SetFloat("Y",-5.3f);
            PlayerPrefs.SetFloat("Z",0);
            SceneManager.LoadScene("NivelFinal");
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