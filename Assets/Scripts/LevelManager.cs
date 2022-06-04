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

       public static void LoadMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }


    public static void LoadPause()
    {
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("MenuPause", LoadSceneMode.Single);
    }


    public static void ReturnGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
    }
}
