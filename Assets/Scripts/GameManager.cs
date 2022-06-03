using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameManager : MonoBehaviour
{

    private void OnEnable()
    {
        return;
    }

    public void OnMouseOverNoButton()
    {
        return;
    }

    public void OnClickPlayGameButton()
    {
        LevelManager.LoadNextLevel();       
    }

    public void OnMouseOverPlayGameButton()
    {
         return; 
    }

    public void OnClickStoreButton()
    {
        LevelManager.LoadStore();
    }

    public void OnClickGoToMenu()
    {
        LevelManager.LoadMenu();
    }
}
