using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelManager : MonoBehaviour
{
    public static void LoadFirstLevel()
    {
        SceneManager.LoadScene("Cinematic");
        PlayerPrefs.SetInt("Coins",0);
        PlayerPrefs.SetInt("Lives",3); 
        PlayerPrefs.DeleteKey("X");
        PlayerPrefs.DeleteKey("Y");
        PlayerPrefs.DeleteKey("Z");
    }

    public static void LoadNextNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); 
    }

    public static void LoadStore()
    {
        SceneManager.LoadScene("Store", LoadSceneMode.Single);
    }

    public static void LoadDeathMenu()
    {
        SceneManager.LoadScene("DeathMenu");
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        PlayerPrefs.SetInt("Coins",0);
        PlayerPrefs.SetInt("Lives",3);
    }

    public static void LoadMenuS()
    {
        SceneManager.LoadScene("Menu");
    }

    public static void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public static void SettingMenu()
    {
        SceneManager.LoadScene("MenuSettings");
    }
    
    public static void RestartBack()
    {
        PlayerPrefs.SetInt("Coins",0);
        PlayerPrefs.SetInt("Lives",3);
        PlayerPrefs.DeleteKey("Level");
        PlayerPrefs.DeleteKey("Skin_0");
        PlayerPrefs.DeleteKey("SelectedSkin");
        PlayerPrefs.DeleteKey("Skin_1");
        SceneManager.LoadScene("Menu");
    }
}
