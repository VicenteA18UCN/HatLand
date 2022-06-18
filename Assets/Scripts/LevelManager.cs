using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelManager : MonoBehaviour
{

    public static void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+3); 
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

    
}
